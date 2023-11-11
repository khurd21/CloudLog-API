using CloudLogAPI.Records;

namespace CloudLogAPI.Entities;

public sealed class ListJumpsResponse
{
    public IEnumerable<LoggedJump>? Jumps { get; init; } 
}