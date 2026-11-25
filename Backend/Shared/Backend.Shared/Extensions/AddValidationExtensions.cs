using System.Reflection;
using Backend.Shared.Validation;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Backend.Shared.Extensions;

public static class AddValidationExtensions
{
    /// <summary>
    /// Registers all validators of a module and the shared validation behavior.
    /// Safe to call once per module: the behavior is only added the first time.
    /// </summary>
    public static void AddRequestValidation(this IServiceCollection services, Assembly assembly)
    {
        services.AddValidatorsFromAssembly(assembly);

        services.TryAddEnumerable(ServiceDescriptor.Transient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>)));
    }
}
