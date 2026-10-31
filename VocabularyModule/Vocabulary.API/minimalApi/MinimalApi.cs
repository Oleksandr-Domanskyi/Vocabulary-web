using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Vocabulary.Application.CQRS.Command;
using Vocabulary.Application.CQRS.Query;
using Vocabulary.Core.DTO.Request;
using Vocabulary.Core.DTO.Request.Vocabulary;

namespace Vocabulary.API.minimalApi;

public static class MinimalApi
{
    public static void MapVocabularyEndpoints(WebApplication app)
    {
        var group = app.MapGroup("/api/vocabulary")
            .WithName("Vocabulary");

        group.MapGet("/", GetAllVocabulary)
            .WithName("GetAllVocabulary");

        group.MapGet("/{Id}", GetVocabularyById)
            .WithName("GetVocabularyById");

        group.MapPost("/", CreateVocabulary)
            .WithName("CreateVocabulary");

        group.MapPut("/{id}", UpdateVocabulary)
            .WithName("UpdateVocabulary");

        group.MapDelete("/{id}", DeleteVocabulary)
            .WithName("DeleteVocabulary");
    }

    private static async Task<IResult> GetAllVocabulary(IMediator mediator)
    {
        var result = await mediator.Send(new GetAllVocabularyQuery());
        return Results.Ok(result);
    }

    private static async Task<IResult> GetVocabularyById(Guid Id, IMediator mediator)
    {
        var result = await mediator.Send(new GetVocabularyByIdQuery(Id));
        return result != null ? Results.Ok(result) : Results.NotFound();
    }

    private static async Task<IResult> CreateVocabulary(CreateVocabularyRequest request, IMediator mediator)
    {

        var result = await mediator.Send(new CreateVocabularyCommand(request));
        return Results.Created($"/api/vocabulary/{result.WordId}", result);
    }

    private static async Task<IResult> UpdateVocabulary(Guid id, UpdateVocabularyRequest request, IMediator mediator)
    {
        var command = new UpdateVocabularyCommand(id, request);
        var result = await mediator.Send(command);
        return Results.Ok(result);
    }

    private static async Task<IResult> DeleteVocabulary(Guid id, IMediator mediator)
    {
        var result = await mediator.Send(new DeleteVocabularyCommand(id));
        return result ? Results.NoContent() : Results.NotFound();
    }
}