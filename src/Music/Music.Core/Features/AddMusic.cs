using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Music.Core.Database;
using Shared.Configuration.Endpoints;

namespace Music.Core.Features;

internal record AddMusicCommand(AddMusicCommand.AddMusicBody Body) : IRequest<Unit>
{
    public record AddMusicBody(string Name, string Description, string ThumbnailUrl, string FileUrl);
}

internal class AddMusicEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
        =>
            app.MapPost("/api/AddMusic",
                async (AddMusicCommand.AddMusicBody body, [FromServices] IMediator mediator) =>
                {
                    await mediator.Send(new AddMusicCommand(body));
                    return Results.NoContent();
                });
}

internal class AddMusicCommandHandler(MusicDbContext dbContext, IMapper mapper) : IRequestHandler<AddMusicCommand, Unit>
{
    public async Task<Unit> Handle(AddMusicCommand request, CancellationToken cancellationToken)
    {
        var newMusic = mapper.Map<Entities.Music>(request.Body);

        await dbContext.Musics.AddAsync(newMusic, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}