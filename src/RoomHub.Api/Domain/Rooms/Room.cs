using RoomHub.Api.Common;
using RoomHub.Api.Domain.Bookings;

namespace RoomHub.Api.Domain.Rooms;

public sealed class Room
{
    private const int LargeRoomCapacityThreshold = 10;
    private static readonly TimeSpan LargeRoomMaxDuration = TimeSpan.FromHours(2);

    private Room()
    {
    }

    public Guid Id { get; private set; }

    public string Name { get; private set; } = string.Empty;

    public int Capacity { get; private set; }

    public static Result<Room> Create(string? name, int capacity)
    {
        if (string.IsNullOrWhiteSpace(name))
            return RoomErrors.InvalidName;

        if (capacity <= 0)
            return RoomErrors.InvalidCapacity;

        return new Room
        {
            Id = Guid.NewGuid(),
            Name = name.Trim(),
            Capacity = capacity
        };
    }

    public Result<Booking> Book(
        string? bookedBy,
        DateTime startUtc,
        DateTime endUtc,
        int attendees,
        DateTime nowUtc,
        IReadOnlyCollection<Booking> existingBookings,
        int participantActiveBookingsOnDay)
    {
        var created = Booking.Create(Id, bookedBy, startUtc, endUtc, attendees, nowUtc);
        if (created.IsFailure)
            return created.Error;

        var booking = created.Value;

        if (attendees > Capacity)
            return BookingErrors.AttendeesExceedCapacity;

        if (Capacity > LargeRoomCapacityThreshold && endUtc - startUtc > LargeRoomMaxDuration)
            return BookingErrors.DurationTooLongForLargeRoom;

        if (existingBookings.Any(existing => existing.IsActive && booking.Overlaps(existing)))
            return BookingErrors.RoomOccupied;

        if (participantActiveBookingsOnDay >= Booking.MaxActivePerParticipantPerDay)
            return BookingErrors.DailyLimitExceeded;

        return booking;
    }
}
