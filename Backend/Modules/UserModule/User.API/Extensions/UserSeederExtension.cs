
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using User.Core.Entity;
using User.Infrastructure.Database;
using User.Infrastructure.Seeder;

namespace User.API.Extensions;

public static class UserSeederExtension
{
    public static async Task<Guid?> SeedUserAsync(this WebApplication app, CancellationToken cancellationToken = default)
    {
        await using var scope = app.Services.CreateAsyncScope();

        var services = scope.ServiceProvider;

        var userManager = services.GetRequiredService<UserManager<UserModel>>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
        var context = services.GetRequiredService<UserDbContext>();

        var userId = await UserSeed.SeedAsync(
            userManager,
            roleManager,
            context,
            app.Configuration,
            cancellationToken);

        return userId == null ? throw new InvalidOperationException("User Seeder returned null UserId") : (Guid)userId;
    }
}
