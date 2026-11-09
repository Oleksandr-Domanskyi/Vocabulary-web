using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using User.Application.Extensions;
using User.Infrastructure.Extensions;

namespace User.API.Extensions;

public static class UserAPIExtension
{
    public static void AddUserModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddUserApplication();
        services.AddUserInfrastructure(configuration);

        services.AddUserIdentity();
        services.AddCorsExtension(configuration);

        services.AddUserAuthentication(configuration);
        services.AddUserAuthorization();
    }
}
