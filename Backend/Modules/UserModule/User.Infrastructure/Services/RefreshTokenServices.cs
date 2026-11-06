using User.Application.Contracts;
using User.Core.Entity;
using User.Infrastructure.Repositories.IRepositories;

namespace User.Infrastructure.Services;

public sealed class RefreshTokenServices(IRefreshTokenRepository repository) : IRefreshTokenService
{
    public Task<RefreshToken?> GetByTokenHashAsync(string tokenHash) => repository.GetByTokenHashAsync(tokenHash);
    public Task<RefreshToken> CreateAsync(RefreshToken token) => repository.CreateAsync(token);
    public Task<bool> RevokeAsync(string tokenHash) => repository.RevokeAsync(tokenHash);

    // Validation

    public async Task<RefreshToken?> GetValidTokenAsync(string tokenHash)
    {
        var token = await repository.GetByTokenHashAsync(tokenHash);

        if (token is null)
            return null;

        if (token.RevokedAt is not null)
            return null;

        if (token.ExpiresAt <= DateTime.UtcNow)
            return null;

        return token;
    }

}
