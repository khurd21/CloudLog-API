using CloudLogAPI.Records;

namespace CloudLogAPI.Entities;

public sealed class EditJumpResponse
{
    public LoggedJump? EditedJump { get; init; }
}