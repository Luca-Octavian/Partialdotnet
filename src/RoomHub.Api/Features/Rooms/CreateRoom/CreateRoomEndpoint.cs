using RoomHub.Api.Common;

namespace RoomHub.Api.Features.Rooms.CreateRoom;

public static class CreateRoomEndpoint
{
    public static void MapCreateRoom(this IEndpointRouteBuilder app) =>
        app.MapPost("/api/rooms", async (
            CreateRoomRequest request,
            ICommandHandler<CreateRoomCommand, RoomResponse> handler,
            CancellationToken cancellationToken) =>
        {
            var result = await handler.Handle(
                new CreateRoomCommand(request.Name, request.Capacity),
                cancellationToken);

            return result.IsFailure
                ? result.Error.ToProblem()
                : Results.Created($"/api/rooms/{result.Value.Id}", result.Value);
        });
}
