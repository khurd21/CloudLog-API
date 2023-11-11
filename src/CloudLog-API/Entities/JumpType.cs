using System.Text.Json;
using System.Text.Json.Serialization;

namespace CloudLogAPI.Entities;

public class JumpTypeConverter : JsonConverter<JumpType>
{
    public override JumpType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.String)
        {
            if (Enum.TryParse(reader.GetString(), true, out JumpType result))
            {
                return result;
            }
        }

        throw new JsonException($"Unable to parse {nameof(JumpType)} from JSON.");
    }

    public override void Write(Utf8JsonWriter writer, JumpType value, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }
}

[JsonConverter(typeof(JumpTypeConverter))]
public enum JumpType
{
    Belly,
    FreeFly,
    WingSuit,
    HighPull,
    Tandem,
    AFF,
    CRW,
    XRW,
}