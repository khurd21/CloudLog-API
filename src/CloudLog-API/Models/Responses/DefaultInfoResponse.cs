using CloudLogAPI.Models.DynamoDB;

namespace CloudLogAPI.Models.Responses;

public sealed class DefaultInfoResponse
{
    public DefaultInfo? DefaultInfo { get; init; }
}