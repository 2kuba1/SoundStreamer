using System.Reflection;
using MassTransit;
using Serilog;
using Serilog.Events;
using Worker;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((host, log) =>
{
    if (host.HostingEnvironment.IsProduction())
        log.MinimumLevel.Information();
    else
        log.MinimumLevel.Debug();

    log.MinimumLevel.Override("Microsoft", LogEventLevel.Warning);
    log.MinimumLevel.Override("Quartz", LogEventLevel.Information);
    log.WriteTo.Console();
});

builder.Services.AddMassTransit(bus =>
{
    bus.SetKebabCaseEndpointNameFormatter();

    Extensions.AddModuleToBus.Add(bus, new List<Assembly?>()
    {
        Assembly.GetAssembly(typeof(Search.Core.Extensions)),
        Assembly.GetAssembly(typeof(Users.Core.Extensions)),
        Assembly.GetAssembly(typeof(FileStreamer.Core.Extensions)),
        Assembly.GetAssembly(typeof(FileStreamer.Core.Extensions)),
        Assembly.GetAssembly(typeof(PlayLists.Core.Extensions)),
        Assembly.GetAssembly(typeof(Music.Core.Extensions)),
    });

    bus.UsingRabbitMq((context, configurator) =>
    {
        configurator.Host(builder.Configuration["RabbitMq:Host"], "/", cfg =>
        {
            cfg.Username(builder.Configuration.GetValue("RabbitMq:Username", "guest"));
            cfg.Password(builder.Configuration.GetValue("RabbitMq:Password", "guest"));
        });
        configurator.ConfigureEndpoints(context);
    });
});

var app = builder.Build();

app.Run();