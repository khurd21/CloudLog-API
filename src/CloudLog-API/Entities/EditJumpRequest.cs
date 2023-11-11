using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using CloudLogAPI.Records;

namespace CloudLogAPI.Entities;

public sealed class EditJumpRequest
{
    public LoggedJump? Jump { get; init; }
}