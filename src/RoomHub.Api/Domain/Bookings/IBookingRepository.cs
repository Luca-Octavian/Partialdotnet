namespace RoomHub.Api.Domain.Bookings;

public interface IBookingRepository
{
    Task<Booking?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<IReadOnlyCollection<Booking>> ListActiveForRoomAsync(Guid roomId, CancellationToken cancellationToken);

    Task AddAsync(Booking booking, CancellationToken cancellationToken);
}
