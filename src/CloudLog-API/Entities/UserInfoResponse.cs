using CloudLogAPI.Records;

namespace CloudLogAPI.Entities;

public sealed class UserInfoResponse
{
    public UserInfo? UserInfo { get; init; }
}