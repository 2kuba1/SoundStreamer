using System.Reflection;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Configuration.Endpoints;
using Users.Core.Database;

namespace Users.Core;

public static class Extensions
{
    public static IServiceCollection AddUsers(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddEndpoints(Assembly.GetExecutingAssembly());

        services.AddDbContext<UserDbContext>(opt =>
            opt.UseNpgsql(configuration.GetConnectionString("UsersConnectionString")));

        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));


        return services;
    }
}