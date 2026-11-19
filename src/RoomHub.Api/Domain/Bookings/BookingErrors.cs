using RoomHub.Api.Common;

namespace RoomHub.Api.Domain.Bookings;

public static class BookingErrors
{
    public static readonly Error BookedByRequired =
        new("Bookings.BookedByRequired", "BookedBy is required.", ErrorType.Validation);

    public static readonly Error InvalidRange =
        new("Bookings.InvalidRange", "The end must be after the start.", ErrorType.Validation);

    public static readonly Error InvalidAttendees =
        new("Bookings.InvalidAttendees", "Attendees must be positive.", ErrorType.Validation);

    public static readonly Error AttendeesExceedCapacity =
        new("Bookings.AttendeesExceedCapacity", "The room does not have enough seats.", ErrorType.Validation);

    public static readonly Error StartInPast =
        new("Bookings.StartInPast", "A booking cannot start in the past.", ErrorType.Validation);

    public static readonly Error DurationTooLong =
        new("Bookings.DurationTooLong", "A booking cannot exceed 4 hours.", ErrorType.Validation);

    public static readonly Error RoomOccupied =
        new("Bookings.RoomOccupied", "The room is occupied in this interval.", ErrorType.Conflict);

    public static readonly Error DailyLimitExceeded =
        new("Bookings.DailyLimitExceeded", "A participant cannot have more than 2 active bookings per day.", ErrorType.Conflict);

    public static readonly Error AlreadyCancelled =
        new("Bookings.AlreadyCancelled", "The booking is already cancelled.", ErrorType.Conflict);

    public static readonly Error NotFound =
        new("Bookings.NotFound", "The booking does not exist.", ErrorType.NotFound);
}
