using CloudLogAPI.Models.DynamoDB;

namespace CloudLogAPI.Models.Requests;

public sealed class DefaultInfoRequest
{
    public DefaultInfo? DefaultInfo { get; init; }
}