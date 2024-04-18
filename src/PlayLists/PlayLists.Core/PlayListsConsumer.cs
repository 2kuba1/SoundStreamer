using FileStreamer.Contracts;
using MassTransit;

namespace PlayLists.Core;

public class PlayListsConsumer : IConsumer<OrderPlaced>
{
    public async Task Consume(ConsumeContext<OrderPlaced> context)
    {
        Console.WriteLine($"-----> {context.Message.OrderId}");
    }
}