using Microsoft.Extensions.DependencyInjection;

namespace FileStreamer.Core;

public static class Extensions
{
    public static IServiceCollection AddFileStream(this IServiceCollection services)
    {
        return services;
    }
}