using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Shared.Configuration.Endpoints;

namespace FileStreamer.Core;

public static class Extensions
{
    public static IServiceCollection AddFileStream(this IServiceCollection services)
    {
        services.AddEndpoints(Assembly.GetExecutingAssembly());

        return services;
    }
}