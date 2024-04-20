using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Shared.Configuration;

namespace PlayLists.Core;

public static class Extensions
{
    public static IServiceCollection AddPlayLists(this IServiceCollection services)
    {
        ConsumersRegistrator.Register(Assembly.GetExecutingAssembly(), services);

        return services;
    }
}