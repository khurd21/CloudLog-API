using CloudLogAPI.Models.DynamoDB;

namespace CloudLogAPI.Services;

public interface IUserInfoService
{
    UserInfo GetUserInfo(string id);

    DefaultInfo GetDefaultInfo(string id);

    void SetUserInfo(UserInfo userInfo);

    void SetDefaultInfo(DefaultInfo defaultInfo);
}