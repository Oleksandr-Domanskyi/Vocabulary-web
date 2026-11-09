using Microsoft.Extensions.DependencyInjection;
using User.Core.Constants;

namespace User.API.Extensions;

public static class UserAuthorizationExtension
{
    public static IServiceCollection AddUserAuthorization(this IServiceCollection services)
    {
        services.AddAuthorizationBuilder()
            .AddPolicy(SubscriptionTypes.Pro, policy =>
                policy.RequireAuthenticatedUser()
                    .RequireClaim(
                        UserClaims.Subscription,
                        SubscriptionTypes.Pro,
                        SubscriptionTypes.Business))
            .AddPolicy(SubscriptionTypes.Business, policy =>
                policy.RequireAuthenticatedUser()
                    .RequireClaim(
                        UserClaims.Subscription,
                        SubscriptionTypes.Business))
            .AddPolicy(SubscriptionTypes.Admin, policy =>
                policy.RequireAuthenticatedUser()
                    .RequireRole(UserRoles.Admin));

        return services;
    }
}
