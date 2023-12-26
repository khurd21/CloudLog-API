using System.ComponentModel.DataAnnotations;
using FoolProof.Core;

namespace CloudLogAPI.Entities;

public sealed class ListJumpsRequest
{
    [Range(1, int.MaxValue, ErrorMessage=$"{nameof(this.From)} must be greater than zero.")]
    public int From { get; init; }

    [GreaterThan(nameof(this.From), ErrorMessage=$"{nameof(this.To)} must be greater than {nameof(this.From)}")]
    public int To { get; init; }
}