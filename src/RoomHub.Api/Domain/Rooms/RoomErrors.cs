using RoomHub.Api.Common;

namespace RoomHub.Api.Domain.Rooms;

public static class RoomErrors
{
    public static readonly Error InvalidName =
        new("Rooms.InvalidName", "Room name is required.", ErrorType.Validation);

    public static readonly Error InvalidCapacity =
        new("Rooms.InvalidCapacity", "Capacity must be positive.", ErrorType.Validation);

    public static readonly Error NotFound =
        new("Rooms.NotFound", "The room does not exist.", ErrorType.NotFound);
}
