using CloudLogAPI.Models.DynamoDB;

namespace CloudLogAPI.Models.Requests;

public sealed class LogJumpRequest
{
    public LoggedJump? Jump { get; init; }
}