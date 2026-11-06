using MediatR;
using User.Application.DTO.Responses.Auth;
using User.Application.Contracts;
using User.Application.Common.Result;
namespace User.Application.CQRS.Auth.LoginUser;

public sealed class LoginUserCommandHandler(IUserAccountService service, IAuthTokenService tokenService) : IRequestHandler<LoginUserCommand, UserResult<AuthTokensResponse>>
{
    public async Task<UserResult<AuthTokensResponse>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var login = await service.LoginAsync(request.Data.Email, request.Data.Password);
        if (!login.Succeeded || login.User is null)
        {
            var code = login.IsLockedOut ? "LockedOut" :
                login.IsNotAllowed ? "NotAllowed" :
                login.RequiresTwoFactor ? "TwoFactorRequired" : "InvalidCredentials";

            return UserResult<AuthTokensResponse>.Fail(code, "Authentication failed.");
        }
        var roles = await service.GetRolesAsync(login.User);
        var claims = await service.GetClaimsAsync(login.User);

        var tokens = await tokenService.IssueAsync(login.User, roles, claims, cancellationToken);
        return UserResult<AuthTokensResponse>.Ok(tokens);
    }
}
