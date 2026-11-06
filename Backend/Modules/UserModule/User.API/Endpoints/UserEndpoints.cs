
using System.Security.Claims;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using User.API.Routes;

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
            .WithDescription(
                "Retrieves the authenticated user's identifier. " +
                "The endpoint will return complete profile information " +
                "once GetUserProfileQuery is implemented.")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized);

        group.MapPut(UserRoutes.UpdateMe, NotImplemented)
            .WithName("UpdateCurrentUser")
            .WithSummary("Update current user profile")
            .WithDescription(
                "Updates the authenticated user's profile information, " +
                "including username and avatar. " +
                "Only the account owner can modify their profile.")
            .ProducesProblem(StatusCodes.Status501NotImplemented)
            .Produces(StatusCodes.Status401Unauthorized);

        group.MapDelete(UserRoutes.DeleteMe, NotImplemented)
            .WithName("DeleteCurrentUser")
            .WithSummary("Delete current user account")
            .WithDescription(
                "Permanently deletes the authenticated user's account. " +
                "Related user activities and refresh tokens are removed " +
                "according to the configured database relationships.")
            .ProducesProblem(StatusCodes.Status501NotImplemented)
            .Produces(StatusCodes.Status401Unauthorized);

        group.MapGet(UserRoutes.ById, NotImplemented)
            .WithName("GetUserById")
            .WithSummary("Get user by ID")
            .WithDescription(
                "Retrieves a user's publicly accessible profile information " +
                "using their unique identifier. " +
                "Sensitive information such as password hashes, security tokens, " +
                "and private claims is excluded.")
            .ProducesProblem(StatusCodes.Status501NotImplemented)
            .Produces(StatusCodes.Status401Unauthorized);

        group.MapGet(UserRoutes.MyRoles, NotImplemented)
            .WithName("GetCurrentUserRoles")
            .WithSummary("Get current user roles")
            .WithDescription(
                "Retrieves all Identity roles assigned to the authenticated user, " +
                "such as User or Admin. " +
                "Roles determine access to protected application resources.")
            .ProducesProblem(StatusCodes.Status501NotImplemented)
            .Produces(StatusCodes.Status401Unauthorized);

        group.MapGet(UserRoutes.MyClaims, NotImplemented)
            .WithName("GetCurrentUserClaims")
            .WithSummary("Get current user claims")
            .WithDescription(
                "Retrieves the authenticated user's claims that are permitted " +
                "for client-side viewing, including subscription information. " +
                "Sensitive security claims are excluded.")
            .ProducesProblem(StatusCodes.Status501NotImplemented)
            .Produces(StatusCodes.Status401Unauthorized);
    }

    // Returns the user ID until GetUserProfileQuery is implemented.
    private static IResult GetCurrentUser(ClaimsPrincipal user)
    {
        var rawId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value
            ?? user.FindFirst("sub")?.Value;

        return Guid.TryParse(rawId, out var userId)
            ? Results.Ok(new { UserId = userId })
            : Results.Unauthorized();
    }

    private static IResult NotImplemented() =>
        Results.Problem(
            statusCode: StatusCodes.Status501NotImplemented,
            title: "Endpoint not implemented");
}
