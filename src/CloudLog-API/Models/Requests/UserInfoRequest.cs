using CloudLogAPI.Models.DynamoDB;

namespace CloudLogAPI.Models.Requests;

public sealed class UserInfoRequest
{
    public UserInfo? UserInfo { get; init; }
}