using RoomHub.Api.Common;
using RoomHub.Api.Domain;
using RoomHub.Api.Domain.Bookings;
using RoomHub.Api.Domain.Rooms;

namespace RoomHub.Api.Features.Bookings.CreateBooking;

public sealed class CreateBookingHandler(
    IRoomRepository rooms,
    IBookingRepository bookings,
    IUnitOfWork unitOfWork,
    TimeProvider timeProvider) : ICommandHandler<CreateBookingCommand, CreateBookingResponse>
{
    public async Task<Result<CreateBookingResponse>> Handle(
        CreateBookingCommand command,
        CancellationToken cancellationToken)
    {
        var room = await rooms.GetByIdAsync(command.RoomId, cancellationToken);
        if (room is null)
            return RoomErrors.NotFound;

        var existingBookings = await bookings.ListActiveForRoomAsync(room.Id, cancellationToken);

        var bookedBy = command.BookedBy?.Trim();
        var participantBookingsThatDay = string.IsNullOrEmpty(bookedBy)
            ? 0
            : await bookings.CountActiveForParticipantOnDayAsync(
                bookedBy,
                command.StartUtc.Date,
                cancellationToken);

        var booked = room.Book(
            command.BookedBy,
            command.StartUtc,
            command.EndUtc,
            command.Attendees,
            timeProvider.GetUtcNow().UtcDateTime,
            existingBookings,
            participantBookingsThatDay);

        if (booked.IsFailure)
            return booked.Error;

        var booking = booked.Value;
        await bookings.AddAsync(booking, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new CreateBookingResponse(booking.Id, booking.RoomId, booking.Status.ToString());
    }
}
