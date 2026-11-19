namespace RoomHub.Api.Common;

public sealed record Error(string Code, string Description, ErrorType Type);
