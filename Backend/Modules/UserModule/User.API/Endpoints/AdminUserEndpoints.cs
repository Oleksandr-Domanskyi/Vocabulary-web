using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using User.API.Routes;
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
                "Retrieves a list of registered users. " +
                "Accessible only to administrators.")
            .ProducesProblem(StatusCodes.Status501NotImplemented);

        group.MapPost(UserRoutes.AdminRoles, NotImplemented)
            .WithName("AdminAddUserRole")
            .WithSummary("Assign a role to a user")
            .WithDescription(
                "Assigns a specified role to an existing user. " +
                "Requires administrator privileges.")
            .ProducesProblem(StatusCodes.Status501NotImplemented);

        group.MapDelete(UserRoutes.AdminRole, NotImplemented)
            .WithName("AdminRemoveUserRole")
            .WithSummary("Remove a role from a user")
            .WithDescription(
                "Removes an assigned role from an existing user. " +
                "Requires administrator privileges.")
            .ProducesProblem(StatusCodes.Status501NotImplemented);

        group.MapPost(UserRoutes.AdminClaims, NotImplemented)
            .WithName("AdminAddUserClaim")
            .WithSummary("Assign a claim to a user")
            .WithDescription(
                "Adds a specified claim to an existing user's identity. " +
                "Claims may define permissions, subscriptions, or other attributes.")
            .ProducesProblem(StatusCodes.Status501NotImplemented);

        group.MapDelete(UserRoutes.AdminClaims, NotImplemented)
            .WithName("AdminRemoveUserClaim")
            .WithSummary("Remove a claim from a user")
            .WithDescription(
                "Removes a specified claim from an existing user's identity. " +
                "Requires administrator privileges.")
            .ProducesProblem(StatusCodes.Status501NotImplemented);

        group.MapPost(UserRoutes.AdminRevokeSessions, NotImplemented)
            .WithName("AdminRevokeUserSessions")
            .WithSummary("Revoke all user sessions")
            .WithDescription(
                "Revokes all active refresh tokens associated with a user. " +
                "Previously issued access tokens require a separate revocation " +
                "mechanism or remain valid until expiration.")
            .ProducesProblem(StatusCodes.Status501NotImplemented);
    }

    private static IResult NotImplemented() =>
        Results.Problem(
            statusCode: StatusCodes.Status501NotImplemented,
            title: "Endpoint not implemented");
}
