using CloudLogAPI.Records;

namespace CloudLogAPI.Entities;

public sealed class UserInfoRequest
{
    public UserInfo? UserInfo { get; init; }
}