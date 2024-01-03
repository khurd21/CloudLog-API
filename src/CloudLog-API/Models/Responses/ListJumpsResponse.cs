using CloudLogAPI.Models.DynamoDB;

namespace CloudLogAPI.Models.Responses;

public sealed class ListJumpsResponse
{
    public IEnumerable<LoggedJump>? Jumps { get; init; } 
}