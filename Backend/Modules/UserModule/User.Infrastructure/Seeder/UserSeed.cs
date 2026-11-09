using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using User.Core.Constants;
using User.Core.Entity;
using User.Infrastructure.Database;

namespace User.Infrastructure.Seeder;

/// <summary>
/// Development demo account: Identity password hashing, role, subscription and activity.
/// Credentials are read from configuration; never hard-code a test password.
/// </summary>
public static class UserSeed
{
    public static async Task<Guid?> SeedAsync(
        UserManager<UserModel> userManager,
        RoleManager<IdentityRole<Guid>> roleManager,
        UserDbContext context,
        IConfiguration configuration,
        CancellationToken cancellationToken = default)
    {
        if (!configuration.GetValue<bool>("SeedUser:Enabled"))
            return null;

        var email = Required(configuration, "SeedUser:Email");
        var userName = Required(configuration, "SeedUser:UserName");
        var password = Required(configuration, "SeedUser:Password");

        await EnsureRoleAsync(roleManager, UserRoles.User);
        await EnsureRoleAsync(roleManager, UserRoles.Admin);

        var user = await userManager.FindByEmailAsync(email);

        if (user is null)
        {
            user = new UserModel
            {
                Id = Guid.NewGuid(),
                Email = email,
                UserName = userName,
                EmailConfirmed = true, // Development-only demo user.
                Level = 1,
                ExperiencePoints = 0,
                CreatedAt = DateTime.UtcNow,
                LastActiveAt = DateTime.UtcNow
            };

            EnsureSuccess(
                await userManager.CreateAsync(user, password),
                "Create demo user");
        }

        if (!await userManager.IsInRoleAsync(user, UserRoles.User))
        {
            EnsureSuccess(
                await userManager.AddToRoleAsync(user, UserRoles.User),
                "Assign User role");
        }

        var claims = await userManager.GetClaimsAsync(user);
        if (!claims.Any(x => x.Type == UserClaims.Subscription))
        {
            EnsureSuccess(
                await userManager.AddClaimAsync(
                    user,
                    new Claim(UserClaims.Subscription, SubscriptionTypes.Default)),
                "Assign default subscription");
        }

        // Seed activity only if the demo user has no activity yet.
        if (!await context.UserActivities
            .AnyAsync(x => x.UserId == user.Id, cancellationToken))
        {
            var baseDate = DateOnly.FromDateTime(user.CreatedAt);
            var activityCounts = new[] { 2, 3, 1, 4, 2, 5, 3 };
            var xp = new[] { 10, 15, 5, 20, 10, 25, 15 };

            var activities = Enumerable.Range(0, activityCounts.Length)
                .Select(i => new UserActivity
                {
                    Id = Guid.NewGuid(),
                    UserId = user.Id,
                    Date = baseDate.AddDays(i - 6),
                    ActivityCount = activityCounts[i],
                    EarnedExperience = xp[i]
                })
                .ToList();

            context.UserActivities.AddRange(activities);
            await context.SaveChangesAsync(cancellationToken);

            // Do not overwrite existing user progression.
            if (user.ExperiencePoints == 0)
            {
                user.ExperiencePoints = activities.Sum(x => x.EarnedExperience);
                EnsureSuccess(
                    await userManager.UpdateAsync(user),
                    "Update demo experience");
            }
        }

        return user.Id;
    }

    private static string Required(IConfiguration configuration, string key) =>
        !string.IsNullOrWhiteSpace(configuration[key])
            ? configuration[key]!
            : throw new InvalidOperationException($"Missing configuration '{key}'.");

    private static async Task EnsureRoleAsync(
        RoleManager<IdentityRole<Guid>> roleManager,
        string name)
    {
        if (await roleManager.RoleExistsAsync(name))
            return;

        EnsureSuccess(
            await roleManager.CreateAsync(new IdentityRole<Guid>(name)),
            $"Create role '{name}'");
    }

    private static void EnsureSuccess(IdentityResult result, string operation)
    {
        if (result.Succeeded)
            return;

        throw new InvalidOperationException(
            $"{operation}: " + string.Join("; ",
                result.Errors.Select(x => $"{x.Code}: {x.Description}")));
    }
}
