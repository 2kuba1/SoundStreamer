using FileStreamer.Contracts;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Shared.Configuration.Endpoints;

namespace FileStreamer.Core;

public class FileStreamEndpoints() : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
        => app.MapGet("/", async (HttpContext context, IPublishEndpoint publishEndpoint) =>
        {
            await publishEndpoint.Publish<OrderPlaced>(new OrderPlaced()
            {
                OrderId = Guid.NewGuid()
            });

            await context.Response.WriteAsync("Order placed");
        });
}