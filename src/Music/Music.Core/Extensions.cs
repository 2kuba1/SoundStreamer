using System.Reflection;
using Mapster;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Music.Core.Database;
using Shared.Configuration;
using Shared.Configuration.Endpoints;

namespace Music.Core;

public static class Extensions
{
    public static IServiceCollection AddMusic(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddEndpoints(Assembly.GetExecutingAssembly());
        ConsumersRegistrator.Register(Assembly.GetExecutingAssembly(), services);

        services.AddDbContext<MusicDbContext>(cfg =>
            cfg.UseNpgsql(configuration.GetConnectionString("MusicConnectionString")));

        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        
        return services;
    }
}