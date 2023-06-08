using System;
using System.Globalization;
using Alpex.Interfaces.Common;
using Newtonsoft.Json;

namespace Alpex.Interfaces.Geometry;

public sealed class AngleDegJsonConverter : JsonConverter
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
                return (AngleDeg)longValue;
            case JsonToken.Float:
                var decimalValue = (decimal)(double)reader.Value;
                return (AngleDeg)decimalValue;
            case JsonToken.String:
            {
                var stringValue = ((string)reader.Value)?.Trim();
                if (string.IsNullOrEmpty(stringValue))
                    throw new JsonConverterDeserializeException(
                        "Unable to convert empty string into " + nameof(AngleDeg), typeof(AngleDeg));
                var result = AngleDeg.Parse(stringValue, NumberFormatInfo.InvariantInfo);
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

        var v = (AngleDeg)value;
        writer.WriteValue(v.Degrees);
    }

    private static readonly Type TypeBase = typeof(AngleDeg);
}
