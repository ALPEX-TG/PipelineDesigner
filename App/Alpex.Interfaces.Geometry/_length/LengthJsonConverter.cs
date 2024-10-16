using System;
using System.Globalization;
using Newtonsoft.Json;

namespace Alpex.Interfaces.Geometry;

public sealed class LengthJsonConverter : JsonConverter<Length>
{
    public static Length FromJson(object value)
    {
        switch (value)
        {
            case long l:
                return Length.FromMeter(l);
            case double d:
                return Length.FromMeter(d);
            case decimal decimalValue:
                return Length.FromMeter(decimalValue);
            case string s:
                return Length.Parse(s, NumberFormatInfo.InvariantInfo).GetValueOrThrow();
        }

        var message = value?.GetType().Name ?? "null";
        throw new NotImplementedException("Unable to deserialize from " + message);
    }

    public static object ToJson(Length value)
    {
        if (value.Unit == LengthUnits.Meter)
        {
            var rounded = Math.Floor(value.Value);
            if (rounded == value.Value)
                return (long)value.Value;
            return value.Value;
        }

        var valueAsString = value.Value.ToString(CultureInfo.InvariantCulture);
        return valueAsString + value.Unit;
    }

    public override Length ReadJson(JsonReader reader, Type objectType, Length existingValue, bool hasExistingValue,
        JsonSerializer serializer)
    {
        var readerValue = reader.Value;
        return FromJson(readerValue);
    }

    public override void WriteJson(JsonWriter writer, Length value, JsonSerializer serializer)
    {
        var json = ToJson(value);
        writer.WriteValue(json);
    }
}
