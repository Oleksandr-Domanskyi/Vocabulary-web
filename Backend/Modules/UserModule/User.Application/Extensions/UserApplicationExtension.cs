using Microsoft.Extensions.DependencyInjection;

namespace User.Application.Extensions;

public static class UserApplicationExtension
{
    public static void AddUserApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(UserApplicationExtension).Assembly));
    }
}
