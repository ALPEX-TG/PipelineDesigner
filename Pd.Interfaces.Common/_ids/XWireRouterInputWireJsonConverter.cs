using System;
using Newtonsoft.Json;

namespace Pd.Interfaces.Common;

public sealed class XWireRouterInputWireJsonConverter : JsonConverter
{
    public override bool CanConvert(Type objectType)
    {
        return objectType == typeof(XWireRouterInputWire);
    }

    public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue,
        JsonSerializer serializer)
    {
        switch (reader.Value)
        {
            case long l:
                return new XWireRouterInputWire((int)l);
            case int i:
                return new XWireRouterInputWire(i);
            case null when objectType == typeof(XWireRouterInputWire?):
                return null;
        }

        throw new NotImplementedException();
    }

    public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
    {
        var v = (XWireRouterInputWire)value;
        writer.WriteValue(v.Value);
    }
}
