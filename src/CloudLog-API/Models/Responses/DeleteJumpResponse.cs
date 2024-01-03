using CloudLogAPI.Models.DynamoDB;

namespace CloudLogAPI.Models.Responses;

public sealed class DeleteJumpResponse
{
    public LoggedJump? DeletedJump { get; init; }
}