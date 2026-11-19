using RoomHub.Api.Common;

namespace RoomHub.Api.Features.Bookings.GetBooking;

public static class GetBookingEndpoint
{
    public static void MapGetBooking(this IEndpointRouteBuilder app) =>
        app.MapGet("/api/bookings/{id:guid}", async (
            Guid id,
            IQueryHandler<GetBookingQuery, BookingResponse> handler,
            CancellationToken cancellationToken) =>
        {
            var result = await handler.Handle(new GetBookingQuery(id), cancellationToken);

            return result.IsFailure
                ? result.Error.ToProblem()
                : Results.Ok(result.Value);
        });
}
