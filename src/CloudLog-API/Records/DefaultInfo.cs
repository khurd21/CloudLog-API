using System.Text.Json.Serialization;
using Amazon.DynamoDBv2.DataModel;

namespace CloudLogAPI.Records;

[DynamoDBTable(nameof(UserInfo))]
public sealed class DefaultInfo
{
    [DynamoDBHashKey]
    [JsonIgnore]
    public string? Id { get; set; }

    [DynamoDBProperty]
    public string? DefaultParachuteType { get; init; }

    [DynamoDBProperty]
    public int? DefaultParachuteSize { get; init; }
}
