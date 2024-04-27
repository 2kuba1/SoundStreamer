using API.Outbox;
using FileStreamer.Core;
using Mapster;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Music.Core;
using PlayLists.Core;
using Search.Core;
using Serilog;
using Shared.Configuration.Endpoints;
using Shared.Services;
using Users.Core;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, cfg) =>
    cfg.ReadFrom.Configuration(context.Configuration));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<OutboxDbContext>(cfg =>
    cfg.UseNpgsql(builder.Configuration.GetConnectionString("OutboxConnectionString")));

builder.Services.AddFileStream();
builder.Services.AddPlayLists();
builder.Services.AddUsers(builder.Configuration);
builder.Services.AddSearch();
builder.Services.AddMusic(builder.Configuration);

builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();

builder.Services.AddMassTransit(bus =>
{
    bus.SetKebabCaseEndpointNameFormatter();

    bus.AddEntityFrameworkOutbox<OutboxDbContext>(cfg =>
    {
        cfg.QueryDelay = TimeSpan.FromSeconds(10);

        cfg.UsePostgres();
        cfg.UseBusOutbox();
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

builder.Services.AddMapster();

var app = builder.Build();

app.MapEndpoints();

app.UseSerilogRequestLogging();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.Run();