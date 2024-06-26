﻿using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Shared.Configuration;
using Shared.Configuration.Endpoints;

namespace PlayLists.Core;

public static class Extensions
{
    public static IServiceCollection AddPlayLists(this IServiceCollection services)
    {
        services.AddEndpoints(Assembly.GetExecutingAssembly());
        ConsumersRegistrator.Register(Assembly.GetExecutingAssembly(), services);

        return services;
    }
}