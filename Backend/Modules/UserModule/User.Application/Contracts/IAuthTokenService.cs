using System.Security.Claims;
using User.Application.DTO.Responses.Auth;
using User.Core.Entity;

namespace User.Application.Contracts;

/// <summary>
/// To be implemented by a secure JWT/refresh-token service in User.Infrastructure.
/// Raw refresh tokens must be hashed before database lookup; refresh rotation must be atomic.
/// </summary>
public interface IAuthTokenService
{
    Task<AuthTokensResponse> IssueAsync(UserModel user, IReadOnlyList<string> roles, IReadOnlyList<Claim> claims, CancellationToken cancellationToken);
    Task<AuthTokensResponse?> RefreshAsync(string rawRefreshToken, CancellationToken cancellationToken);
    Task<bool> LogoutAsync(string rawRefreshToken, CancellationToken cancellationToken);
}
