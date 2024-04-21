using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Shared.Configuration.Endpoints;

namespace Users.Core;

public static class Extensions
{
    public static IServiceCollection AddUsers(this IServiceCollection services)
    {
        services.AddEndpoints(Assembly.GetExecutingAssembly());
        
        return services;
    }
}