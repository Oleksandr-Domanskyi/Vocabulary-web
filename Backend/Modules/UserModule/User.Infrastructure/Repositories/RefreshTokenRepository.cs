
using Microsoft.EntityFrameworkCore;
using User.Core.Entity;
using User.Infrastructure.Database;
using User.Infrastructure.Repositories.IRepositories;

namespace User.Infrastructure.Repositories;

public class RefreshTokenRepository(UserDbContext context) : IRefreshTokenRepository
{
    public Task<RefreshToken?> GetByTokenHashAsync(string tokenHash)
    {
        return context.RefreshTokens
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.TokenHash == tokenHash);
    }

    public async Task<RefreshToken> CreateAsync(RefreshToken token)
    {
        context.RefreshTokens.Add(token);

        await context.SaveChangesAsync();

        return token;
    }

    public async Task<bool> RevokeAsync(string tokenHash)
    {
        var affectedRows = await context.RefreshTokens
            .Where(x =>
                x.TokenHash == tokenHash &&
                x.RevokedAt == null)
            .ExecuteUpdateAsync(x =>
                x.SetProperty(
                    token => token.RevokedAt,
                    DateTime.UtcNow));

        return affectedRows > 0;
    }
}
