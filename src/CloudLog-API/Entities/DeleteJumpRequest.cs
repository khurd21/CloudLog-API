using System.ComponentModel.DataAnnotations;

namespace CloudLogAPI.Entities;

public sealed class DeleteJumpRequest
{
    [Required(ErrorMessage=$"{nameof(this.JumpNumber)} is a required field.")]
    public int JumpNumber { get; init; }
}