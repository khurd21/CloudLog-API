using CloudLogAPI.Models.DynamoDB;

namespace CloudLogAPI.Models.Responses;

public sealed class EditJumpResponse
{
    public LoggedJump? EditedJump { get; init; }
}