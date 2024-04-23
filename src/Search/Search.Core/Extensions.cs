using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Shared.Configuration;
using Shared.Configuration.Endpoints;

namespace Search.Core;

public static class Extensions
{
    public static IServiceCollection AddSearch(this IServiceCollection services)
    {
        services.AddEndpoints(Assembly.GetExecutingAssembly());
        ConsumersRegistrator.Register(Assembly.GetExecutingAssembly(), services);
        
        return services;
    }
}