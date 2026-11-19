using RoomHub.Api.Common;

namespace RoomHub.Api.Domain.Bookings;

public sealed class Booking
{
    private static readonly TimeSpan MaxDuration = TimeSpan.FromHours(4);

    public const int MaxActivePerParticipantPerDay = 2;

    private static readonly TimeSpan MinCancellationNotice = TimeSpan.FromMinutes(60);

    private Booking()
    {
    }

    public Guid Id { get; private set; }

    public Guid RoomId { get; private set; }

    public string BookedBy { get; private set; } = string.Empty;

    public DateTime StartUtc { get; private set; }

    public DateTime EndUtc { get; private set; }

    public int Attendees { get; private set; }

    public BookingStatus Status { get; private set; }

    public bool IsActive => Status == BookingStatus.Active;

    internal static Result<Booking> Create(
        Guid roomId,
        string? bookedBy,
        DateTime startUtc,
        DateTime endUtc,
        int attendees,
        DateTime nowUtc)
    {
        if (string.IsNullOrWhiteSpace(bookedBy))
            return BookingErrors.BookedByRequired;

        if (endUtc <= startUtc)
            return BookingErrors.InvalidRange;

        if (attendees <= 0)
            return BookingErrors.InvalidAttendees;

        if (startUtc < nowUtc)
            return BookingErrors.StartInPast;

        if (endUtc - startUtc > MaxDuration)
            return BookingErrors.DurationTooLong;

        return new Booking
        {
            Id = Guid.NewGuid(),
            RoomId = roomId,
            BookedBy = bookedBy.Trim(),
            StartUtc = startUtc,
            EndUtc = endUtc,
            Attendees = attendees,
            Status = BookingStatus.Active
        };
    }

    public bool Overlaps(Booking other) => StartUtc < other.EndUtc && other.StartUtc < EndUtc;

    public Result Cancel(DateTime nowUtc)
    {
        if (Status == BookingStatus.Cancelled)
            return BookingErrors.AlreadyCancelled;

        if (StartUtc < nowUtc.Add(MinCancellationNotice))
            return BookingErrors.CancellationTooLate;

        Status = BookingStatus.Cancelled;
        return Result.Success();
    }
}
