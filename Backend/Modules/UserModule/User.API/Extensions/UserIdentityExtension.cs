
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using User.Core.Entity;
using User.Infrastructure.Database;

namespace User.API.Extensions;

public static class UserIdentityExtension
{
    public static IServiceCollection AddUserIdentity(this IServiceCollection services)
    {
        services.AddIdentityCore<UserModel>(options =>
        {
            // Password
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireUppercase = true;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequiredLength = 8;
            options.Password.RequiredUniqueChars = 4;

            // Lockout
            options.Lockout.DefaultLockoutTimeSpan =
                TimeSpan.FromMinutes(15);

            options.Lockout.MaxFailedAccessAttempts = 5;
            options.Lockout.AllowedForNewUsers = true;

            // User
            options.User.RequireUniqueEmail = true;

            options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyz" +
                "ABCDEFGHIJKLMNOPQRSTUVWXYZ" +
                "0123456789-._@+";

            // SignIn
            options.SignIn.RequireConfirmedEmail = false;
            options.SignIn.RequireConfirmedPhoneNumber = false;
            options.SignIn.RequireConfirmedAccount = false;

            // Tokens
            options.Tokens.EmailConfirmationTokenProvider = TokenOptions.DefaultProvider;
            options.Tokens.PasswordResetTokenProvider = TokenOptions.DefaultProvider;
        })
        .AddRoles<IdentityRole<Guid>>()
        .AddEntityFrameworkStores<UserDbContext>()
        .AddSignInManager()
        .AddDefaultTokenProviders();

        return services;
    }
}
