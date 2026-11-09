
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace User.API.Extensions;

public static class UserAuthenticationExtension
{
    public static IServiceCollection AddUserAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var issuer = configuration["Jwt:Issuer"]
            ?? throw new InvalidOperationException(
                "JWT Issuer is not configured.");

        var audience = configuration["Jwt:Audience"]
            ?? throw new InvalidOperationException(
                "JWT Audience is not configured.");

        var key = configuration["Jwt:Key"]
            ?? throw new InvalidOperationException(
                "JWT Key is not configured.");

        if (Encoding.UTF8.GetByteCount(key) < 32)
            throw new InvalidOperationException("JWT Key must contain at least 32 bytes.");

        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.MapInboundClaims = false;

                options.TokenValidationParameters =
                    new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = issuer,

                        ValidateAudience = true,
                        ValidAudience = audience,

                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),

                        ValidateLifetime = true,
                        RequireExpirationTime = true,
                        RequireSignedTokens = true,

                        ClockSkew = TimeSpan.Zero,

                        NameClaimType = "sub",
                        RoleClaimType = "role"
                    };
            });

        return services;
    }
}
