using RoomHub.Api.Common;
using RoomHub.Api.Domain;
using RoomHub.Api.Domain.Rooms;

namespace RoomHub.Api.Features.Rooms.CreateRoom;

public sealed class CreateRoomHandler(IRoomRepository rooms, IUnitOfWork unitOfWork)
    : ICommandHandler<CreateRoomCommand, RoomResponse>
{
    public async Task<Result<RoomResponse>> Handle(CreateRoomCommand command, CancellationToken cancellationToken)
    {
        var created = Room.Create(command.Name, command.Capacity);
        if (created.IsFailure)
            return created.Error;

        await rooms.AddAsync(created.Value, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return RoomResponse.From(created.Value);
    }
}
