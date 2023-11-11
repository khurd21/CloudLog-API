using CloudLogAPI.Records;

namespace CloudLogAPI.Entities;

public sealed class DeleteJumpResponse
{
    public LoggedJump? DeletedJump { get; init; }
}