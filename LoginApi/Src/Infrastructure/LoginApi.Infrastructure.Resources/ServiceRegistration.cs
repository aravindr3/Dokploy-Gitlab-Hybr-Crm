using HyBrForex.Application.Interfaces;
using HyBrCRM.Infrastructure.Resources.Services;
using Microsoft.Extensions.DependencyInjection;

namespace HyBrCRM.Infrastructure.Resources;

public static class ServiceRegistration
{
    public static IServiceCollection AddResourcesInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<ITranslator, Translator>();
        return services;
    }
}