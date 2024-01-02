using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Amazon.DynamoDBv2.DataModel;
using CloudLogAPI.Entities;

namespace CloudLogAPI.Records;

[DynamoDBTable(nameof(LoggedJump))]
public sealed class LoggedJump
{
    [DynamoDBHashKey]
    [JsonIgnore]
    public string? Id { get; set; }

    [DynamoDBRangeKey]
    [Required]
    public int? JumpNumber { get; init; }

    [DynamoDBProperty]
    [Required]
    public DateTime? Date { get; init; }

    [DynamoDBProperty]
    [Required]
    public JumpType JumpType { get; init; }

    [DynamoDBProperty]
    public string? Aircraft { get; init; }

    [DynamoDBProperty]
    public int? Altitude { get; init; }

    [DynamoDBProperty]
    public int? PullAltitude { get; init; }

    [DynamoDBProperty]
    public int? WindSpeedKnots { get; init; }

    [DynamoDBProperty]
    public string? Parachute { get; init; }

    [DynamoDBProperty]
    [Range(29, 500, ErrorMessage = $"{nameof(this.ParachuteSize)} must be greater than 29 and less than 500")]
    public int? ParachuteSize { get; init; }

    [DynamoDBProperty]
    public string? Dropzone { get; init; }

    [DynamoDBProperty]
    public string? Description { get; init; }

    [DynamoDBProperty]
    [Required]
    public string? SignedBy { get; init; }

    [DynamoDBProperty]
    [Required]
    public string? SignersLicenseNumber { get; init; }
}