using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Amazon.DynamoDBv2.DataModel;

namespace CloudLogAPI.Models.DynamoDB;

[DynamoDBTable(nameof(UserInfo))]
public sealed class UserInfo
{
    [DynamoDBHashKey]
    [JsonIgnore]
    public string? Id { get; set; }

    [DynamoDBProperty]
    public string? FirstName { get; init; }

    [DynamoDBProperty]
    public string? LastName { get; init; }

    [DynamoDBProperty]
    public int? USPAMembershipNumber { get; init; }

    [DynamoDBProperty]
    [RegularExpression(
        @"^[A-D]-\d+$",
        ErrorMessage=$"{nameof(this.USPALicenseNumber)} must be in the format `A-123456`, `B-123456`, `C-123456`, or `D-123456`")]
    public string? USPALicenseNumber { get; init; }
}