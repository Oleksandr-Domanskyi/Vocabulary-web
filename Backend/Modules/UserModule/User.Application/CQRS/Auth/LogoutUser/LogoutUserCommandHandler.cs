using MediatR;
using User.Application.Common.Result;
using User.Application.Contracts;

namespace User.Application.CQRS.Auth.LogoutUser;

public sealed class LogoutUserCommandHandler(IAuthTokenService tokenService) : IRequestHandler<LogoutUserCommand, UserResult<bool>>
{
    public async Task<UserResult<bool>> Handle(LogoutUserCommand request, CancellationToken cancellationToken)
    {
        var revoked = await tokenService.LogoutAsync(request.Data.RefreshToken, cancellationToken);
        return UserResult<bool>.Ok(revoked);
    }
}
