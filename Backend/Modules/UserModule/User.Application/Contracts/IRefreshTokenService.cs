using User.Core.Entity;

namespace User.Application.Contracts;

/// <summary>Application-facing contract for RefreshTokenRepositoryServices.</summary>
public interface IRefreshTokenService
{
    Task<RefreshToken?> GetByTokenHashAsync(string tokenHash);
    Task<RefreshToken?> GetValidTokenAsync(string tokenHash);
    Task<RefreshToken> CreateAsync(RefreshToken token);
    Task<bool> RevokeAsync(string tokenHash);
}
