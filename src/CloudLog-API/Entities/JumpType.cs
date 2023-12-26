using System.Text.Json.Serialization;

namespace CloudLogAPI.Entities;

[Flags]
public enum JumpType
{
    Belly = 0,
    FreeFly = 1,
    WingSuit = 2,
    HighPull = 3,
    Tandem = 4,
    AFF = 5,
    CRW = 6,
    XRW = 7,
}