using System;
using System.Globalization;
using Newtonsoft.Json;
using Pd.Interfaces.Common;

namespace Pd.Interfaces.Geometry;

public sealed class DiffMinuteAngleJsonConverter : JsonConverter
{
    public override bool CanConvert(Type objectType)
    {
        return objectType == TypeBase;
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
        JsonSerializer serializer)
    {
        Console.WriteLine(reader.TokenType);
        switch (reader.TokenType)
        {
            case JsonToken.Integer:
                var longValue = (long)reader.Value;
                return (DiffMinuteAngle)longValue;
            case JsonToken.Float:
                var decimalValue = (decimal)(double)reader.Value;
                return (DiffMinuteAngle)decimalValue;
            case JsonToken.String:
            {
                var stringValue = ((string)reader.Value)?.Trim();
                if (string.IsNullOrEmpty(stringValue))
                    throw new JsonConverterDeserializeException(
                        "Unable to convert empty string into " + nameof(DiffMinuteAngle), typeof(DiffMinuteAngle));
                var result = DiffMinuteAngle.Parse(stringValue, NumberFormatInfo.InvariantInfo);
                return result.GetValueOrThrow();
            }
        }

        throw new JsonConverterDeserializeException($"token \'{reader.TokenType}\'", TypeBase);
    }


    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        if (value == null)
        {
            writer.WriteNull();
            return;
        }

        var v = (DiffMinuteAngle)value;
        writer.WriteValue(v.Minutes);
    }

    private static readonly Type TypeBase = typeof(DiffMinuteAngle);
}
