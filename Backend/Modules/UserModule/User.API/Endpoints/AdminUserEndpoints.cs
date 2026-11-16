
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using User.API.Routes;
using User.Application.Common.Result;
using User.Application.CQRS.Admin.AddUserClaim;
using User.Application.CQRS.Admin.AddUserRole;
using User.Application.CQRS.Admin.RemoveUserClaim;
using User.Application.CQRS.Admin.RemoveUserRole;
using User.Application.DTO.Requests.Admin;
using User.Core.Constants;

namespace User.API.Endpoints;

public static class AdminUserEndpoints
{
    public static void MapAdminUserEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup(UserRoutes.AdminBase)
            .WithTags("Admin Users")
            .RequireAuthorization(new AuthorizeAttribute
            {
                Roles = UserRoles.Admin
            });

        group.MapGet(UserRoutes.AdminAll, NotImplemented)
            .WithName("AdminGetUsers")
            .WithSummary("Get all users")
            .WithDescription(
                "Retrieves a paginated list of registered users. " +
                "Accessible only to administrators.")
            .ProducesProblem(StatusCodes.Status501NotImplemented)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden);

        group.MapPost(UserRoutes.AdminRoles, AddUserRole)
            .WithName("AdminAddUserRole")
            .WithSummary("Assign a role to a user")
            .WithDescription(
                "Assigns a specified role to an existing user.")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status409Conflict);

        group.MapDelete(UserRoutes.AdminRole, RemoveUserRole)
            .WithName("AdminRemoveUserRole")
            .WithSummary("Remove a role from a user")
            .WithDescription(
                "Removes an assigned role from an existing user.")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status409Conflict);

        group.MapPost(UserRoutes.AdminClaims, AddUserClaim)
            .WithName("AdminAddUserClaim")
            .WithSummary("Assign a claim to a user")
            .WithDescription(
                "Adds a specified claim to an existing user's identity.")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status409Conflict);

        group.MapDelete(UserRoutes.AdminClaims, RemoveUserClaim)
            .WithName("AdminRemoveUserClaim")
            .WithSummary("Remove a claim from a user")
            .WithDescription(
                "Removes the specified claim from an existing user. " +
                "Claim type and value must be provided in the request body.")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status409Conflict);

        group.MapPost(
                UserRoutes.AdminRevokeSessions,
                NotImplemented)
            .WithName("AdminRevokeUserSessions")
            .WithSummary("Revoke all user sessions")
            .WithDescription(
                "Revokes all active refresh tokens associated with a user. " +
                "Existing access tokens require a separate revocation mechanism " +
                "or remain valid until expiration.")
            .ProducesProblem(StatusCodes.Status501NotImplemented)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden);
    }

    private static async Task<IResult> AddUserRole(Guid id, AddUserRoleRequest data, IMediator mediator, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new AddUserRoleCommand(id, data), cancellationToken);

        if (!result.IsSuccess)
            return ToProblem(result, "Assign user role failed");

        return Results.NoContent();
    }

    private static async Task<IResult> RemoveUserRole(Guid id, string role, IMediator mediator, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new RemoveUserRoleCommand(id, role), cancellationToken);

        if (!result.IsSuccess)
            return ToProblem(result, "Remove user role failed");

        return Results.NoContent();
    }

    private static async Task<IResult> AddUserClaim(Guid id, AddUserClaimRequest data, IMediator mediator, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new AddUserClaimCommand(id, data), cancellationToken);

        if (!result.IsSuccess)
            return ToProblem(result, "Assign user claim failed");

        return Results.NoContent();
    }

    private static async Task<IResult> RemoveUserClaim(Guid id, [FromBody] RemoveUserClaimRequest data, IMediator mediator, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new RemoveUserClaimCommand(id, data), cancellationToken);

        if (!result.IsSuccess)
            return ToProblem(result, "Remove user claim failed");

        return Results.NoContent();
    }

    private static IResult ToProblem<T>(UserResult<T> result, string title)
    {
        var statusCode = result.Errors.Any(
            e => e.Code == "NotFound")
                ? StatusCodes.Status404NotFound
                : result.Errors.Any(e => e.Code is
                    "ClaimAlreadyExists" or
                    "UserAlreadyInRole" or
                    "ConcurrencyFailure")
                    ? StatusCodes.Status409Conflict
                    : StatusCodes.Status400BadRequest;

        return Results.Problem(
            detail: string.Join(
                "; ",
                result.Errors.Select(e => e.Message)),
            statusCode: statusCode,
            title: title);
    }

    private static IResult NotImplemented() =>
        Results.Problem(
            statusCode: StatusCodes.Status501NotImplemented,
            title: "Endpoint not implemented");
}
