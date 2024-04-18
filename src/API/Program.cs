using FileStreamer.Core;
using MassTransit;
using PlayLists.Core;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddFileStream();
builder.Services.AddPlayLists();

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

app.AddEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.Run();