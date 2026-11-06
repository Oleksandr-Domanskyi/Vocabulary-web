using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Vocabulary.API.Routes;
using Vocabulary.Application.CQRS.Command.CreateVocabulary;
using Vocabulary.Application.CQRS.Command.DeleteVocabulary;
using Vocabulary.Application.CQRS.Command.UpdateVocabulary;
using Vocabulary.Application.CQRS.Query.GetAllVocabulary;
using Vocabulary.Application.CQRS.Query.GetVocabularyById;
using Vocabulary.Core.DTO.Request.Vocabulary;

namespace Vocabulary.API.Endpoints;

public static class VocabularyEndpoints
{
    public static void MapVocabularyEndpoints(WebApplication app)
    {
        var group = app.MapGroup(VocabularyRoutes.BaseRoute)
            .WithTags(RouteNames.Base);

        group.MapGet(VocabularyRoutes.GetAllRoute, GetAllVocabulary)
            .WithName(RouteNames.GetAllVocabulary)
            .WithSummary("Get all vocabulary words")
            .WithDescription(
                "Retrieves a collection of vocabulary words, including " +
                "their definitions, translations, examples, dictionaries, " +
                "and additional vocabulary information.")
            .Produces(StatusCodes.Status200OK);

        group.MapGet(VocabularyRoutes.GetByIdRoute, GetVocabularyById)
            .WithName(RouteNames.GetVocabularyById)
            .WithSummary("Get vocabulary word by ID")
            .WithDescription(
                "Retrieves a specific vocabulary word using its unique identifier. " +
                "The response includes word details, examples, translations, " +
                "definitions, and related dictionary references.")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);

        group.MapPost(VocabularyRoutes.CreateRoute, CreateVocabulary)
            .WithName(RouteNames.CreateVocabulary)
            .WithSummary("Create a vocabulary word")
            .WithDescription(
                "Creates a new vocabulary word with its definition, transcription, " +
                "English proficiency level, word type, examples, translations, " +
                "and dictionary references. Translation examples are linked " +
                "to their corresponding original examples.")
            .Produces(StatusCodes.Status201Created);

        group.MapPut(VocabularyRoutes.UpdateRoute, UpdateVocabulary)
            .WithName(RouteNames.UpdateVocabulary)
            .WithSummary("Update a vocabulary word")
            .WithDescription(
                "Updates an existing vocabulary word identified by its unique ID. " +
                "Supports modifying word details, definitions, examples, " +
                "translations, and related dictionary references.")
            .Produces(StatusCodes.Status200OK);

        group.MapDelete(VocabularyRoutes.DeleteRoute, DeleteVocabulary)
            .WithName(RouteNames.DeleteVocabulary)
            .WithSummary("Delete a vocabulary word")
            .WithDescription(
                "Permanently deletes a vocabulary word using its unique identifier. " +
                "Related records are deleted according to the configured " +
                "database cascade delete relationships.")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound);
    }

    private static async Task<IResult> GetAllVocabulary(IMediator mediator)
    {
        var result = await mediator.Send(new GetAllVocabularyQuery());
        return Results.Ok(result);
    }

    private static async Task<IResult> GetVocabularyById(
        Guid id,
        IMediator mediator)
    {
        var result = await mediator.Send(new GetVocabularyByIdQuery(id));
        return result != null ? Results.Ok(result) : Results.NotFound();
    }

    private static async Task<IResult> CreateVocabulary(
        CreateVocabularyRequest request,
        IMediator mediator)
    {
        var result = await mediator.Send(
            new CreateVocabularyCommand(request));

        return Results.Created($"{VocabularyRoutes.BaseRoute}/{result.WordId}", result);
    }

    private static async Task<IResult> UpdateVocabulary(
        Guid id,
        UpdateVocabularyRequest request,
        IMediator mediator)
    {
        var command = new UpdateVocabularyCommand(id, request);
        var result = await mediator.Send(command);

        return Results.Ok(result);
    }

    private static async Task<IResult> DeleteVocabulary(
        Guid id,
        IMediator mediator)
    {
        var result = await mediator.Send(new DeleteVocabularyCommand(id));
        return result ? Results.NoContent() : Results.NotFound();
    }
}
