using RoomHub.Api.Common;
using RoomHub.Api.Domain.Rooms;

namespace RoomHub.Api.Features.Rooms.GetRoom;

public sealed class GetRoomHandler(IRoomRepository rooms) : IQueryHandler<GetRoomQuery, RoomResponse>
{
    public async Task<Result<RoomResponse>> Handle(GetRoomQuery query, CancellationToken cancellationToken)
    {
        var room = await rooms.GetByIdAsync(query.Id, cancellationToken);
        if (room is null)
            return RoomErrors.NotFound;

        return RoomResponse.From(room);
    }
}
