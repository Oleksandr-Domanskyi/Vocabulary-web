
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using User.API.Routes;

namespace User.API.Endpoints;

public static class AuthEndpoints
{
    public static void MapAuthEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup(AuthRoutes.Base)
            .WithTags("Auth");

        group.MapPost(AuthRoutes.Register, NotImplemented)
            .WithName("RegisterUser")
            .WithSummary("Register a new user")
            .WithDescription(
                "Creates a new user account with the default User role " +
                "and subscription claims.")
            .ProducesProblem(StatusCodes.Status501NotImplemented);

        group.MapPost(AuthRoutes.Login, NotImplemented)
            .WithName("LoginUser")
            .WithSummary("Authenticate a user")
            .WithDescription(
                "Authenticates a user using email and password. " +
                "Upon successful authentication, issues an access token " +
                "and a refresh token.")
            .ProducesProblem(StatusCodes.Status501NotImplemented);

        group.MapPost(AuthRoutes.Refresh, NotImplemented)
            .WithName("RefreshUserToken")
            .WithSummary("Refresh authentication tokens")
            .WithDescription(
                "Validates an existing refresh token and issues a new " +
                "access token and refresh token pair. " +
                "The previous refresh token is revoked.")
            .ProducesProblem(StatusCodes.Status501NotImplemented);

        group.MapPost(AuthRoutes.Logout, NotImplemented)
            .WithName("LogoutUser")
            .WithSummary("Log out the current user")
            .WithDescription(
                "Revokes the current user's refresh token to prevent " +
                "further token renewal. The existing access token " +
                "remains valid until expiration unless separately revoked.")
            .RequireAuthorization()
            .ProducesProblem(StatusCodes.Status501NotImplemented);

        group.MapPost(AuthRoutes.ConfirmEmail, NotImplemented)
            .WithName("ConfirmUserEmail")
            .WithSummary("Confirm user email address")
            .WithDescription(
                "Confirms a user's email address using a verification " +
                "token issued during registration.")
            .ProducesProblem(StatusCodes.Status501NotImplemented);

        group.MapPost(AuthRoutes.ForgotPassword, NotImplemented)
            .WithName("ForgotUserPassword")
            .WithSummary("Request a password reset")
            .WithDescription(
                "Initiates the password recovery process by generating " +
                "a password reset token and sending recovery instructions " +
                "to the user's email address.")
            .ProducesProblem(StatusCodes.Status501NotImplemented);

        group.MapPost(AuthRoutes.ResetPassword, NotImplemented)
            .WithName("ResetUserPassword")
            .WithSummary("Reset user password")
            .WithDescription(
                "Resets the user's password using a valid password reset " +
                "token and a new password.")
            .ProducesProblem(StatusCodes.Status501NotImplemented);
    }

    // Replace placeholders with MediatR commands when handlers are implemented.
    private static IResult NotImplemented() =>
        Results.Problem(
            statusCode: StatusCodes.Status501NotImplemented,
            title: "Endpoint not implemented");
}
