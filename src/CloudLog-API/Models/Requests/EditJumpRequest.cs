using CloudLogAPI.Models.DynamoDB;

namespace CloudLogAPI.Models.Requests;

public sealed class EditJumpRequest
{
    public LoggedJump? Jump { get; init; }
}