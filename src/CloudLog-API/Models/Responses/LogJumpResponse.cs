using CloudLogAPI.Models.DynamoDB;

namespace CloudLogAPI.Models.Responses;

public sealed class LogJumpResponse
{
    public LoggedJump? LoggedJump { get; init; }
}