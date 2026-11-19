using RoomHub.Api.Common;
using RoomHub.Api.Features.Bookings.CancelBooking;
using RoomHub.Api.Features.Bookings.CreateBooking;
using RoomHub.Api.Features.Bookings.GetBooking;
using RoomHub.Api.Features.Rooms;
using RoomHub.Api.Features.Rooms.CreateRoom;
using RoomHub.Api.Features.Rooms.GetRoom;

namespace RoomHub.Api.Features;

public static class DependencyInjection
{
    public static IServiceCollection AddFeatures(this IServiceCollection services)
    {
        services.AddScoped<ICommandHandler<CreateRoomCommand, RoomResponse>, CreateRoomHandler>();
        services.AddScoped<IQueryHandler<GetRoomQuery, RoomResponse>, GetRoomHandler>();
        services.AddScoped<ICommandHandler<CreateBookingCommand, CreateBookingResponse>, CreateBookingHandler>();
        services.AddScoped<IQueryHandler<GetBookingQuery, BookingResponse>, GetBookingHandler>();
        services.AddScoped<ICommandHandler<CancelBookingCommand>, CancelBookingHandler>();
        return services;
    }
}
