using CloudLogAPI.Models.DynamoDB;

namespace CloudLogAPI.Models.Responses;

public sealed class UserInfoResponse
{
    public UserInfo? UserInfo { get; init; }
}