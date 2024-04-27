using MapsterMapper;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Shared.Configuration.Endpoints;
using Users.Contracts;
using Users.Core.Database;
using Users.Core.Entities;

namespace Users.Core.Features;

internal record RegisterCommand(RegisterCommand.RegisterCommandBody Body) : IRequest<Unit>
{
    public record RegisterCommandBody(
        string Username,
        string FirstName,
        string LastName,
        string Email,
        string Password,
        string ConfirmPassword);
}

internal class RegisterEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
        =>
            app.MapPost("/api/users/register",
                async (RegisterCommand.RegisterCommandBody body, [FromServices] IMediator mediator) =>
                {
                    await mediator.Send(new RegisterCommand(body));
                    return Results.NoContent();
                });
}

internal class RegisterCommandHandler(UserDbContext dbContext, IPublishEndpoint publishEndpoint, IMapper mapper)
    : IRequestHandler<RegisterCommand, Unit>
{
    public async Task<Unit> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var newUser = mapper.Map<User>(request.Body);

        newUser.HashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Body.Password);

        await dbContext.Users.AddAsync(newUser, cancellationToken);
        var created = await dbContext.SaveChangesAsync(cancellationToken) > 0;

        if (created)
            await publishEndpoint.Publish(
                new UserCreated(newUser.Id, newUser.Username, newUser.FirstName, newUser.LastName, newUser.Email,
                    newUser.EmailConfirmed, (int)newUser.Role), cancellationToken);

        return Unit.Value;
    }
}