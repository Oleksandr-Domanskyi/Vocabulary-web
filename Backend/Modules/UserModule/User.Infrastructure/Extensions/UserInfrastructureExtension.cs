using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using User.Infrastructure.Database;
using User.Infrastructure.Repositories;
using User.Infrastructure.Repositories.IRepositories;
using Backend.Shared.Extensions;
using User.Application.Contracts;
using User.Infrastructure.Services;
using User.Infrastructure.Options;
using System.Text;
namespace User.Infrastructure.Extensions;

public static class UserInfrastructureExtension
{
    public static void AddUserInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDatabase<UserDbContext>(configuration);

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
        services.AddScoped<IUserActivityRepository, UserActivityRepository>();
        services.AddScoped<IUserActivityService, UserActivityService>();
        services.AddScoped<IUserAccountService, UserAccountService>();
        services.AddScoped<IAuthTokenService, AuthTokenService>();
        services.AddScoped<IRefreshTokenService, RefreshTokenService>();

        services.AddOptions<JwtOptions>()
            .Bind(configuration.GetSection(JwtOptions.SectionName))
            .Validate(options =>
                !string.IsNullOrWhiteSpace(options.Issuer) &&
                !string.IsNullOrWhiteSpace(options.Audience) &&
                Encoding.UTF8.GetByteCount(options.Key) >= 32 &&
                options.AccessTokenMinutes > 0 &&
                options.RefreshTokenDays > 0,
                "Invalid JWT configuration.")
            .ValidateOnStart();
    }
}
