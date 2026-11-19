using Microsoft.EntityFrameworkCore;
using RoomHub.Api.Domain.Bookings;

namespace RoomHub.Api.Infrastructure.Persistence;

public sealed class BookingRepository(RoomHubDbContext context) : IBookingRepository
{
    public Task<Booking?> GetByIdAsync(Guid id, CancellationToken cancellationToken) =>
        context.Bookings.FirstOrDefaultAsync(booking => booking.Id == id, cancellationToken);

    public async Task<IReadOnlyCollection<Booking>> ListActiveForRoomAsync(
        Guid roomId,
        CancellationToken cancellationToken) =>
        await context.Bookings
            .Where(booking => booking.RoomId == roomId && booking.Status == BookingStatus.Active)
            .ToListAsync(cancellationToken);

    public Task<int> CountActiveForParticipantOnDayAsync(
        string bookedBy,
        DateTime dayStartUtc,
        CancellationToken cancellationToken)
    {
        var dayEndUtc = dayStartUtc.AddDays(1);

        return context.Bookings.CountAsync(
            booking => booking.BookedBy == bookedBy
                       && booking.Status == BookingStatus.Active
                       && booking.StartUtc >= dayStartUtc
                       && booking.StartUtc < dayEndUtc,
            cancellationToken);
    }

    public async Task AddAsync(Booking booking, CancellationToken cancellationToken) =>
        await context.Bookings.AddAsync(booking, cancellationToken);
}
