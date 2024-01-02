using CloudLogAPI.Records;

namespace CloudLogAPI.Entities;

public sealed class EditJumpRequest
{
    public LoggedJump? Jump { get; init; }
}