using RoomHub.Api.Common;

namespace RoomHub.Api.Features.Rooms.GetRoom;

public static class GetRoomEndpoint
{
    public static void MapGetRoom(this IEndpointRouteBuilder app) =>
        app.MapGet("/api/rooms/{id:guid}", async (
            Guid id,
            IQueryHandler<GetRoomQuery, RoomResponse> handler,
            CancellationToken cancellationToken) =>
        {
            var result = await handler.Handle(new GetRoomQuery(id), cancellationToken);

            return result.IsFailure
                ? result.Error.ToProblem()
                : Results.Ok(result.Value);
        });
}
