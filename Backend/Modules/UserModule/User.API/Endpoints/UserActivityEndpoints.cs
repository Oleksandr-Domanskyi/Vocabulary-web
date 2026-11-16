
using System.Globalization;
using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using User.API.Routes;
using User.Application.Common.Result;
using User.Application.CQRS.Activity.GetCurrentUserActivity;
using User.Application.CQRS.Activity.GetCurrentUserActivityByDate;
using User.Application.CQRS.Activity.GetCurrentUserStatistics;
using User.Application.CQRS.Activity.GetCurrentUserStreak;
using User.Application.DTO.Responses.Activity;

namespace User.API.Endpoints;

public static class UserActivityEndpoints
{
    public static void MapUserActivityEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup(UserActivityRoutes.Base)
            .WithTags("User Activity")
            .RequireAuthorization();

        group.MapGet(
                UserActivityRoutes.Activity,
                GetCurrentUserActivity)
            .WithName("GetCurrentUserActivity")
            .WithSummary("Get current user activity")
            .WithDescription(
                "Retrieves the authenticated user's daily activity history, " +
                "including activity counts and earned experience points. " +
                "Results can be used to build an activity calendar.")
            .Produces<UserActivityHistoryResponse>(
                StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound);

        group.MapGet(
                UserActivityRoutes.ActivityByDate,
                GetCurrentUserActivityByDate)
            .WithName("GetCurrentUserActivityByDate")
            .WithSummary("Get user activity by date")
            .WithDescription(
                "Retrieves the authenticated user's activity for a specific date. " +
                "The date must be provided in YYYY-MM-DD format.")
            .Produces<UserActivityResponse>(
                StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound);

        group.MapGet(
                UserActivityRoutes.Streak,
                GetCurrentUserStreak)
            .WithName("GetCurrentUserStreak")
            .WithSummary("Get current user streak")
            .WithDescription(
                "Retrieves the authenticated user's current streak " +
                "of consecutive active days.")
            .Produces<UserStreakResponse>(
                StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound);

        group.MapGet(
                UserActivityRoutes.Statistics,
                GetCurrentUserStatistics)
            .WithName("GetCurrentUserStatistics")
            .WithSummary("Get current user statistics")
            .WithDescription(
                "Retrieves the authenticated user's learning statistics, " +
                "including level, experience points, activity count, " +
                "and current streak.")
            .Produces<UserStatisticsResponse>(
                StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound);
    }

    private static async Task<IResult> GetCurrentUserActivity(ClaimsPrincipal user, IMediator mediator, CancellationToken cancellationToken)
    {
        if (!TryGetUserId(user, out var userId))
            return Results.Unauthorized();

        var result = await mediator.Send(
            new GetCurrentUserActivityQuery(userId),
            cancellationToken);

        if (!result.IsSuccess)
            return ToProblem(result, "Get user activity failed");

        return Results.Ok(result.Value);
    }

    private static async Task<IResult> GetCurrentUserActivityByDate(string date, ClaimsPrincipal user, IMediator mediator, CancellationToken cancellationToken)
    {
        if (!TryGetUserId(user, out var userId))
            return Results.Unauthorized();

        if (!DateOnly.TryParseExact(
                date,
                "yyyy-MM-dd",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out var activityDate))
        {
            return Results.Problem(
                detail: "Date must be in YYYY-MM-DD format.",
                statusCode: StatusCodes.Status400BadRequest,
                title: "Invalid date format");
        }

        var result = await mediator.Send(new GetCurrentUserActivityByDateQuery(userId, activityDate), cancellationToken);

        if (!result.IsSuccess)
            return ToProblem(result, "Get user activity by date failed");

        return Results.Ok(result.Value);
    }

    private static async Task<IResult> GetCurrentUserStreak(ClaimsPrincipal user, IMediator mediator, CancellationToken cancellationToken)
    {
        if (!TryGetUserId(user, out var userId))
            return Results.Unauthorized();

        var result = await mediator.Send(
            new GetCurrentUserStreakQuery(userId),
            cancellationToken);

        if (!result.IsSuccess)
            return ToProblem(result, "Get user streak failed");

        return Results.Ok(result.Value);
    }

    private static async Task<IResult> GetCurrentUserStatistics(ClaimsPrincipal user, IMediator mediator, CancellationToken cancellationToken)
    {
        if (!TryGetUserId(user, out var userId))
            return Results.Unauthorized();

        var result = await mediator.Send(new GetCurrentUserStatisticsQuery(userId), cancellationToken);

        if (!result.IsSuccess)
            return ToProblem(result, "Get user statistics failed");

        return Results.Ok(result.Value);
    }

    private static bool TryGetUserId(ClaimsPrincipal user, out Guid userId)
    {
        var rawId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value
            ?? user.FindFirst("sub")?.Value;

        return Guid.TryParse(rawId, out userId)
            && userId != Guid.Empty;
    }

    private static IResult ToProblem<T>(UserResult<T> result, string title)
    {
        var statusCode = result.Errors.Any(
            e => e.Code == "NotFound")
                ? StatusCodes.Status404NotFound
                : StatusCodes.Status400BadRequest;

        return Results.Problem(
            detail: string.Join(
                "; ",
                result.Errors.Select(e => e.Message)),
            statusCode: statusCode,
            title: title);
    }
}
