using System.Text.Json;
using Backend.Shared.Errors;
using FluentValidation;
using Vocabulary.Application.Common.Exeptions;
using Vocabulary.Infrastructure.Common.Exeptions;

namespace Backend.API.Middleware;

/// <summary>
/// Converts exceptions thrown while processing a request to problem details responses.
/// </summary>
public class ExceptionHandlingMiddleware(RequestDelegate next,ILogger<ExceptionHandlingMiddleware> logger,IHostEnvironment environment)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        // There is nobody to answer if the response has started or the client has disconnected.
        catch (Exception exception) when (!context.Response.HasStarted && !context.RequestAborted.IsCancellationRequested)
        {
            await ToResult(exception).ExecuteAsync(context);
        }
    }

    private IResult ToResult(Exception exception)
    {
        switch (exception)
        {
            case ValidationException validationException:
                return ToValidationProblem(validationException);

            case VocabularyException vocabularyException:
                return ToProblem(vocabularyException.Error, exception);

            case InfrastructureException infrastructureException:
                return ToProblem(infrastructureException.Error, exception);

            // Malformed request, e.g. invalid JSON. Thrown by minimal APIs in Development.
            case BadHttpRequestException badRequestException:
                return Results.Problem(
                    detail: badRequestException.Message,
                    statusCode: badRequestException.StatusCode);

            default:
                logger.LogError(exception, "Unhandled exception.");

                // Internal details must not reach clients outside Development.
                return Results.Problem(
                    detail: environment.IsDevelopment() ? exception.ToString() : CommonErrors.UnexpectedError.Message,
                    statusCode: StatusCodes.Status500InternalServerError);
        }
    }

    private static IResult ToValidationProblem(ValidationException exception)
    {
        var errors = exception.Errors
            .GroupBy(x => ToJsonPath(x.PropertyName))
            .ToDictionary(
                group => group.Key,
                group => group.Select(x => x.ErrorMessage).Distinct().ToArray());

        return Results.ValidationProblem(errors);
    }

    private IResult ToProblem(Error error, Exception exception)
    {
        var statusCode = error.Type switch
        {
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
            ErrorType.Forbidden => StatusCodes.Status403Forbidden,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            _ => StatusCodes.Status500InternalServerError
        };

        if (statusCode == StatusCodes.Status500InternalServerError)
            logger.LogError(exception, "Application error {Code}.", error.Code);

        return Results.Problem(detail: error.Message, statusCode: statusCode);
    }

    // "Translations[0].TranslatedWord" -> "translations[0].translatedWord"
    private static string ToJsonPath(string propertyName) =>
        string.Join('.', propertyName.Split('.').Select(JsonNamingPolicy.CamelCase.ConvertName));
}
