using Microsoft.EntityFrameworkCore;
using RoomHub.Api.Domain.Rooms;

namespace RoomHub.Api.Infrastructure.Persistence;

public sealed class RoomRepository(RoomHubDbContext context) : IRoomRepository
{
    public Task<Room?> GetByIdAsync(Guid id, CancellationToken cancellationToken) =>
        context.Rooms.FirstOrDefaultAsync(room => room.Id == id, cancellationToken);

    public async Task AddAsync(Room room, CancellationToken cancellationToken) =>
        await context.Rooms.AddAsync(room, cancellationToken);
}
