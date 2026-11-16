
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using User.API.Routes;
using User.Application.CQRS.Auth.LoginUser;
using User.Application.CQRS.Auth.LogoutUser;
using User.Application.CQRS.Auth.RefreshUserToken;
using User.Application.CQRS.Auth.RegisterUser;
using User.Application.DTO.Requests.Auth;

namespace User.API.Endpoints;

public static class AuthEndpoints
{
    public static void MapAuthEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup(AuthRoutes.Base)
            .WithTags("Auth");

        group.MapPost(AuthRoutes.Register, Register)
            .WithName("RegisterUser")
            .WithSummary("Register a new user")
            .WithDescription(
                "Creates a new user account with the default User role " +
                "and subscription claims.")
            .ProducesProblem(StatusCodes.Status501NotImplemented);

        group.MapPost(AuthRoutes.Login, Login)
            .WithName("LoginUser")
            .WithSummary("Authenticate a user")
            .WithDescription(
                "Authenticates a user using email and password. " +
                "Upon successful authentication, issues an access token " +
                "and a refresh token.")
            .ProducesProblem(StatusCodes.Status501NotImplemented);

        group.MapPost(AuthRoutes.Refresh, Refresh)
            .WithName("RefreshUserToken")
            .WithSummary("Refresh authentication tokens")
            .WithDescription(
                "Validates an existing refresh token and issues a new " +
                "access token and refresh token pair. " +
                "The previous refresh token is revoked.")
            .ProducesProblem(StatusCodes.Status501NotImplemented);

        group.MapPost(AuthRoutes.Logout, Logout)
            .WithName("LogoutUser")
            .WithSummary("Log out the current user")
            .WithDescription(
                "Revokes the current user's refresh token to prevent " +
                "further token renewal. The existing access token " +
                "remains valid until expiration unless separately revoked.")
            .RequireAuthorization()
            .ProducesProblem(StatusCodes.Status501NotImplemented);

        group.MapPost(AuthRoutes.ConfirmEmail, ConfirmEmail)
            .WithName("ConfirmUserEmail")
            .WithSummary("Confirm user email address")
            .WithDescription(
                "Confirms a user's email address using a verification " +
                "token issued during registration.")
            .ProducesProblem(StatusCodes.Status501NotImplemented);

        group.MapPost(AuthRoutes.ForgotPassword, ForgotPassword)
            .WithName("ForgotUserPassword")
            .WithSummary("Request a password reset")
            .WithDescription(
                "Initiates the password recovery process by generating " +
                "a password reset token and sending recovery instructions " +
                "to the user's email address.")
            .ProducesProblem(StatusCodes.Status501NotImplemented);

        group.MapPost(AuthRoutes.ResetPassword, ResetPassword)
            .WithName("ResetUserPassword")
            .WithSummary("Reset user password")
            .WithDescription(
                "Resets the user's password using a valid password reset " +
                "token and a new password.")
            .ProducesProblem(StatusCodes.Status501NotImplemented);
    }

    private static async Task<IResult> Register([FromBody] RegisterUserRequest request, IMediator mediator)
    {
        var result = await mediator.Send(new RegisterUserCommand(request));

        if (!result.IsSuccess)
        {
            return Results.Problem(
                detail: string.Join("; ", result.Errors.Select(e => e.Message)),
                statusCode: StatusCodes.Status400BadRequest,
                title: "User registration failed");
        }
        return Results.Ok(
            "User registered successfully. Please check your email to confirm your account."
        );
    }

    private static async Task<IResult> Login([FromBody] LoginUserRequest request, IMediator mediator)
    {
        var result = await mediator.Send(new LoginUserCommand(request));

        if (!result.IsSuccess)
        {
            return Results.Problem(
                detail: string.Join("; ", result.Errors.Select(e => e.Message)),
                statusCode: StatusCodes.Status400BadRequest,
                title: "User login failed");
        }
        return Results.Ok(result.Value);
    }
    private static async Task<IResult> Logout([FromBody] LogoutUserRequest request, IMediator mediator)
    {
        var result = await mediator.Send(new LogoutUserCommand(request));

        if (!result.IsSuccess)
        {
            return Results.Problem(
                detail: string.Join("; ", result.Errors.Select(e => e.Message)),
                statusCode: StatusCodes.Status400BadRequest,
                title: "User logout failed");
        }
        return Results.Ok("User logged out successfully.");
    }

    private static async Task<IResult> Refresh([FromBody] RefreshTokenRequest request, IMediator mediator)
    {
        var result = await mediator.Send(new RefreshUserTokenCommand(request));

        if (!result.IsSuccess)
        {
            return Results.Problem(
                detail: string.Join("; ", result.Errors.Select(e => e.Message)),
                statusCode: StatusCodes.Status400BadRequest,
                title: "Token refresh failed");
        }
        return Results.Ok(result.Value);
    }

    //!Not Realized Endpoints
    private static async Task<IResult> ResetPassword([FromBody] ResetPasswordRequest request, IMediator mediator)
        => Results.Problem(
                    statusCode: StatusCodes.Status501NotImplemented,
                    title: "Endpoint not implemented");

    private static async Task<IResult> ForgotPassword([FromBody] ForgotPasswordRequest request, IMediator mediator)
    => Results.Problem(
                statusCode: StatusCodes.Status501NotImplemented,
                title: "Endpoint not implemented");

    private static async Task<IResult> ConfirmEmail([FromBody] ConfirmEmailRequest request, IMediator mediator)
        => Results.Problem(
                    statusCode: StatusCodes.Status501NotImplemented,
                    title: "Endpoint not implemented");

}
