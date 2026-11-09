using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Backend.Shared.Extensions;

public static class AddSharedExtensions
{
    public static void AddDatabase<TContext>(this IServiceCollection services, IConfiguration configuration)
        where TContext : DbContext
    {
        services.AddDbContext<TContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("Default"),
                b => b.MigrationsAssembly(typeof(TContext).Assembly.FullName)));
    }
}
