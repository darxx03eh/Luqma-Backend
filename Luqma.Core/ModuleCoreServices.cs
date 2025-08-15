using FluentValidation;
using Luqma.Core.Behaviors;
using Luqma.Core.Mapping.Authentications;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Luqma.Core
{
    public static class ModuleCoreServices
    {
        public static IServiceCollection AddModuleCoreServices(this IServiceCollection services)
        {
            // Configuration of Mediator
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
            // Configuation of AutoMapper
            services.AddAutoMapper(Assembly.GetExecutingAssembly());


            // Get Validators
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly(), ServiceLifetime.Scoped);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            return services;
        }
    }
}
