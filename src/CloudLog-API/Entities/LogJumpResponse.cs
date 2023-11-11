using CloudLogAPI.Records;

namespace CloudLogAPI.Entities;

public sealed class LogJumpResponse
{
    public LoggedJump? LoggedJump { get; init; }
}