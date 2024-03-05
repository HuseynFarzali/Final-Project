using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using SMS.WebTools.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SMS.WebTools.Tools
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInjectibles(this IServiceCollection services)
        {
            var assemblies = new List<Assembly>() {
                Assembly.Load("SMS.API"),
                Assembly.Load("SMS.DAL"),
                Assembly.Load("SMS.BLL"),
                Assembly.Load("SMS.Tools"),
                Assembly.Load("SMS.WebTools"),
                Assembly.Load("SMS.Authentication")
            };

            foreach (var assembly in assemblies)
            {
                RegisterInjectiblesToContainer(assembly, services);
            }

            return services;

            static void RegisterInjectiblesToContainer(
                Assembly assembly, IServiceCollection services)
            {
                // Get all types defined with Injectible attribute.
                var injectibles = assembly.GetTypes().Where(type => type.IsDefined(typeof(InjectibleAttribute), false));

                // Register all types with Injectible attribute to the container.
                foreach (var injectibleService in injectibles)
                {
                    var injectibleServiceAttr = injectibleService.GetCustomAttribute<InjectibleAttribute>();

                    var lifetime = injectibleServiceAttr?.Lifetime
                        ?? throw new ArgumentNullException(nameof(injectibleServiceAttr.Lifetime));

                    var implementation = injectibleServiceAttr?.Implements;

                    if (implementation is null)
                    {
                        switch (lifetime)
                        {
                            case ServiceLifetime.Transient:
                                services.AddTransient(injectibleService);
                                break;

                            case ServiceLifetime.Scoped:
                                services.AddScoped(injectibleService);
                                break;

                            case ServiceLifetime.Singleton:
                                services.AddSingleton(injectibleService);
                                break;
                        }
                    }
                    else
                    {
                        switch (lifetime)
                        {
                            case ServiceLifetime.Transient:
                                services.AddTransient(implementation, injectibleService);
                                break;

                            case ServiceLifetime.Scoped:
                                services.AddScoped(implementation, injectibleService);
                                break;

                            case ServiceLifetime.Singleton:
                                services.AddSingleton(implementation, injectibleService);
                                break;
                        }
                    }
                }
            }
        }
    }
}
