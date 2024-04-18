using FileStreamer.Contracts;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace FileStreamer.Core;

public static class FileStreamEndpoints
{
    public static WebApplication AddEndpoints(this WebApplication app)
    {
        app.MapGet("/", async context =>
        {
            using var scope = app.Services.CreateScope();
            var publishEndpoint = scope.ServiceProvider.GetService<IPublishEndpoint>();

            await publishEndpoint.Publish<OrderPlaced>(new OrderPlaced()
            {
                OrderId = Guid.NewGuid()
            });

            await context.Response.WriteAsync("Order placed");
        });

        return app;
    }
}