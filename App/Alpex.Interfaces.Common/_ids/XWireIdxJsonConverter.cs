using System;
using Newtonsoft.Json;

namespace Alpex.Interfaces.Common;

public sealed class XWireIdxJsonConverter : JsonConverter
{
    public override bool CanConvert(Type objectType)
    {
        return objectType == typeof(XWireIdx);
    }

    public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue,
        JsonSerializer serializer)
    {
        switch (reader.Value)
        {
            case long l:
                return new XWireIdx((int)l);
            case int i:
                return new XWireIdx(i);
            case null when objectType == typeof(XWireIdx?):
                return null;
        }

        throw new NotImplementedException();
    }

    public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
    {
        var v = (XWireIdx)value;
        writer.WriteValue(v.Value);
    }
}
