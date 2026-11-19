namespace RoomHub.Api.Common;

public interface IQueryHandler<in TQuery, TResponse>
{
    Task<Result<TResponse>> Handle(TQuery query, CancellationToken cancellationToken);
}
