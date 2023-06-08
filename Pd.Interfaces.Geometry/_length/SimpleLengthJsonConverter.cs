using System;
using System.Globalization;
using Newtonsoft.Json;

namespace Pd.Interfaces.Geometry;

public sealed class SimpleLengthJsonConverter : JsonConverter<SimpleLength>
{
    public static SimpleLength FromJson(object value)
    {
        switch (value)
        {
            case long longValue:
                return new SimpleLength((decimal)longValue);
            case double doubleValue:
                return new SimpleLength((decimal)doubleValue);
            case decimal decimalValue:
                return new SimpleLength(decimalValue);
            case string s:
            {
                var l = Length.Parse(s, NumberFormatInfo.InvariantInfo).GetValueOrThrow();
                return l.ToSimpleLength();
            }
        }

        var message = value?.GetType().Name ?? "null";
        throw new NotImplementedException("Unable to deserialize from " + message);
    }

    public static object ToJson(SimpleLength value)
    {
        var meters  = value.Meters;
        var rounded = Math.Floor(meters);
        if (rounded.Equals(meters))
            return (long)meters;
        return meters;
    }


    public override SimpleLength ReadJson(JsonReader reader, Type objectType, SimpleLength existingValue,
        bool hasExistingValue,
        JsonSerializer serializer)
    {
        var readerValue = reader.Value;
        return FromJson(readerValue);
    }

    public override void WriteJson(JsonWriter writer, SimpleLength value, JsonSerializer serializer)
    {
        var json = ToJson(value);
        writer.WriteValue(json);
    }
}
