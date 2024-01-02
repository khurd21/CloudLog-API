using CloudLogAPI.Records;

namespace CloudLogAPI.Repositories;

public interface IUserInfoService
{
    UserInfo GetUserInfo(string id);

    DefaultInfo GetDefaultInfo(string id);

    void SetUserInfo(UserInfo userInfo);

    void SetDefaultInfo(DefaultInfo defaultInfo);
}