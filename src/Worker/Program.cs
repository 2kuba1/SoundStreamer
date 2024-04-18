using FileStreamer.Core;
using MassTransit;
using PlayLists.Core;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMassTransit(bus =>
{
    bus.SetKebabCaseEndpointNameFormatter();

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