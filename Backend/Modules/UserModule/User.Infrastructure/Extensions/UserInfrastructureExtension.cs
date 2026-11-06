using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using User.Infrastructure.Database;
using User.Infrastructure.Repositories;
using User.Infrastructure.Repositories.IRepositories;
namespace User.Infrastructure.Extensions;

public static class UserInfrastructureExtension
{
    public static void AddUserInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<UserDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
        services.AddScoped<IUserActivityRepository, UserActivityRepository>();
    }
}
