using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using User.Core.Constants;

namespace User.API.Extensions;

public static class CorsExtension
{
    private const string PolicyName = "FrontendPolicy";

    public static IServiceCollection AddCorsExtension(this IServiceCollection services, IConfiguration configuration)
    {
        var origins = configuration
            .GetSection("Cors:AllowedOrigins")
            .Get<string[]>()
            ?? throw new InvalidOperationException("CORS allowed origins are not configured.");

        if (origins.Length == 0)
            throw new InvalidOperationException("At least one CORS origin must be configured.");

        services.AddCors(options =>
            options.AddPolicy(PolicyName, policy => policy.WithOrigins(origins).AllowAnyHeader().AllowAnyMethod()));

        return services;
    }

    public static WebApplication UseCorsExtension(this WebApplication app)
    {
        app.UseCors(PolicyName);

        return app;
    }
}
