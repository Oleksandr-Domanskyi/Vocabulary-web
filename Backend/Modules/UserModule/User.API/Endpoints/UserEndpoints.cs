
using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using User.API.Routes;
using User.Application.Common.Result;
using User.Application.CQRS.Users.DeleteCurrentUser;
using User.Application.CQRS.Users.GetCurrentUser;
using User.Application.CQRS.Users.GetCurrentUserClaims;
using User.Application.CQRS.Users.GetCurrentUserRoles;
using User.Application.CQRS.Users.GetUserById;
using User.Application.CQRS.Users.UpdateCurrentUser;
using User.Application.DTO.Requests.Users;
using User.Application.DTO.Responses.Users;

namespace User.API.Endpoints;

public static class UserEndpoints
{
    public static void MapUserEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup(UserRoutes.Base)
            .WithTags("Users")
            .RequireAuthorization();

        group.MapGet(UserRoutes.Me, GetCurrentUser)
            .WithName("GetCurrentUser")
            .WithSummary("Get current user")
            .WithDescription("Retrieves the authenticated user's profile.")
            .Produces<UserProfileResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized)
            .ProducesProblem(StatusCodes.Status404NotFound);

        group.MapPut(UserRoutes.UpdateMe, UpdateCurrentUser)
            .WithName("UpdateCurrentUser")
            .WithSummary("Update current user profile")
            .WithDescription("Updates the authenticated user's username and avatar.")
            .Produces<UserProfileResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status409Conflict);

        group.MapDelete(UserRoutes.DeleteMe, DeleteCurrentUser)
            .WithName("DeleteCurrentUser")
            .WithSummary("Delete current user account")
            .WithDescription("Permanently deletes the authenticated user's account.")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status401Unauthorized)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound);

        group.MapGet(UserRoutes.ById, GetUserById)
            .WithName("GetUserById")
            .WithSummary("Get user by ID")
            .WithDescription("Retrieves a user's public profile.")
            .Produces<PublicUserProfileResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized)
            .ProducesProblem(StatusCodes.Status404NotFound);

        group.MapGet(UserRoutes.MyRoles, GetRoles)
            .WithName("GetCurrentUserRoles")
            .WithSummary("Get current user roles")
            .WithDescription(
                "Retrieves roles assigned to the authenticated user.")
            .Produces<UserRolesResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized)
            .ProducesProblem(StatusCodes.Status404NotFound);

        group.MapGet(UserRoutes.MyClaims, GetClaims)
            .WithName("GetCurrentUserClaims")
            .WithSummary("Get current user claims")
            .WithDescription(
                "Retrieves non-sensitive claims of the authenticated user.")
            .Produces<UserClaimsResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized)
            .ProducesProblem(StatusCodes.Status404NotFound);
    }

    private static async Task<IResult> GetCurrentUser(ClaimsPrincipal user, IMediator mediator, CancellationToken cancellationToken)
    {
        if (!TryGetUserId(user, out var userId))
            return Results.Unauthorized();

        var result = await mediator.Send(
            new GetCurrentUserQuery(userId),
            cancellationToken);

        if (!result.IsSuccess)
            return ToProblem(result, "Get current user failed");

        return Results.Ok(result.Value);
    }

    private static async Task<IResult> GetUserById(Guid id, IMediator mediator, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(
            new GetUserByIdQuery(id),
            cancellationToken);

        if (!result.IsSuccess)
            return ToProblem(result, "Get user by ID failed");

        return Results.Ok(result.Value);
    }

    private static async Task<IResult> GetRoles(ClaimsPrincipal user, IMediator mediator, CancellationToken cancellationToken)
    {
        if (!TryGetUserId(user, out var userId))
            return Results.Unauthorized();

        var result = await mediator.Send(
            new GetCurrentUserRolesQuery(userId),
            cancellationToken);

        if (!result.IsSuccess)
            return ToProblem(result, "Get user roles failed");

        return Results.Ok(result.Value);
    }

    private static async Task<IResult> GetClaims(ClaimsPrincipal user, IMediator mediator, CancellationToken cancellationToken)
    {
        if (!TryGetUserId(user, out var userId))
            return Results.Unauthorized();

        var result = await mediator.Send(
            new GetCurrentUserClaimsQuery(userId),
            cancellationToken);

        if (!result.IsSuccess)
            return ToProblem(result, "Get user claims failed");

        return Results.Ok(result.Value);
    }

    private static async Task<IResult> UpdateCurrentUser(ClaimsPrincipal user, UpdateUserProfileRequest data, IMediator mediator, CancellationToken cancellationToken)
    {
        if (!TryGetUserId(user, out var userId))
            return Results.Unauthorized();

        var result = await mediator.Send(
            new UpdateCurrentUserCommand(userId, data),
            cancellationToken);

        if (!result.IsSuccess)
            return ToProblem(result, "Update user failed");

        return Results.Ok(result.Value);
    }

    private static async Task<IResult> DeleteCurrentUser(ClaimsPrincipal user, IMediator mediator, CancellationToken cancellationToken)
    {
        if (!TryGetUserId(user, out var userId))
            return Results.Unauthorized();

        var result = await mediator.Send(
            new DeleteCurrentUserCommand(userId),
            cancellationToken);

        if (!result.IsSuccess)
            return ToProblem(result, "Delete user failed");

        return Results.NoContent();
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
                : result.Errors.Any(e => e.Code is
                    "DuplicateUserName" or
                    "DuplicateEmail" or
                    "ConcurrencyFailure")
                    ? StatusCodes.Status409Conflict
                    : StatusCodes.Status400BadRequest;

        return Results.Problem(
            detail: string.Join("; ", result.Errors.Select(e => e.Message)),
            statusCode: statusCode,
            title: title);
    }
}
