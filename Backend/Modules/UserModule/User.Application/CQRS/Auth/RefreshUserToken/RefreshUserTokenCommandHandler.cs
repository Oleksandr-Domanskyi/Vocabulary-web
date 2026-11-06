using MediatR;
using User.Application.DTO.Responses.Auth;
using User.Application.Contracts;
using User.Application.Common.Result;
namespace User.Application.CQRS.Auth.RefreshUserToken;

public sealed class RefreshUserTokenCommandHandler(IAuthTokenService tokenService) : IRequestHandler<RefreshUserTokenCommand, UserResult<AuthTokensResponse>>
{
    public async Task<UserResult<AuthTokensResponse>> Handle(RefreshUserTokenCommand request, CancellationToken cancellationToken)
    {
        var tokens = await tokenService.RefreshAsync(request.Data.RefreshToken, cancellationToken);
        return tokens is null
            ? UserResult<AuthTokensResponse>.Fail("InvalidRefreshToken", "Refresh token is invalid or expired.")
            : UserResult<AuthTokensResponse>.Ok(tokens);
    }
}
