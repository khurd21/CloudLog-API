using Amazon.DynamoDBv2.DataModel;
using CloudLogAPI.Exceptions;
using CloudLogAPI.Records;
using Microsoft.IdentityModel.Tokens;

namespace CloudLogAPI.Repositories;

public class UserInfoService : IUserInfoService
{
    private IDynamoDBContext DynamoDBContext { get; init; }

    public UserInfoService(IDynamoDBContext dynamoDBContext)
    {
        this.DynamoDBContext = dynamoDBContext;
    }

    public DefaultInfo GetDefaultInfo(string id)
    {
        var userDefaultInfo = this.DynamoDBContext
            .LoadAsync<DefaultInfo>(id)
            .Result;
        
        if (userDefaultInfo == null)
        {
            throw new CloudLogException("Default info must be set first.");
        }

        return userDefaultInfo;
    }

    public UserInfo GetUserInfo(string id)
    {
        var userInfo = this.DynamoDBContext
            .LoadAsync<UserInfo>(id)
            .Result;
        
        if (userInfo == null)
        {
            throw new CloudLogException("User info must be set first.");
        }

        return userInfo;
    }

    public void SetDefaultInfo(DefaultInfo defaultInfo)
    {
        if (defaultInfo.Id == null)
        {
            throw new CloudLogException("Identifier for jumper is required.");
        }
        this.DynamoDBContext
            .SaveAsync(defaultInfo)
            .Wait();
    }

    public void SetUserInfo(UserInfo userInfo)
    {
        if (userInfo.Id == null)
        {
            throw new CloudLogException("Identifier for jumper is required.");
        }

        this.DynamoDBContext
            .SaveAsync(userInfo)
            .Wait();
    }
}