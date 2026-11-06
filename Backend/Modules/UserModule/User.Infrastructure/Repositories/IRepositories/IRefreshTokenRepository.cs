using User.Core.Entity;

namespace User.Infrastructure.Repositories.IRepositories;

public interface IRefreshTokenRepository
{
    Task<RefreshToken?> GetByTokenHashAsync(string tokenHash);
    Task<RefreshToken> CreateAsync(RefreshToken token);
    Task<bool> RevokeAsync(string tokenHash);
}

