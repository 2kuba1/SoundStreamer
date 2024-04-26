using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Music.Core.Database;

namespace Music.Core;

public static class Extensions
{
    public static IServiceCollection AddMusic(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<MusicDbContext>(cfg =>
            cfg.UseNpgsql(configuration.GetConnectionString("MusicConnectionString")));

        return services;
    }
}