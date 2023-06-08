using System;
using Newtonsoft.Json;

namespace Pd.Interfaces.Common;

public sealed class XDataTypeUidJsonConverter : JsonConverter
{
    public override bool CanConvert(Type objectType)
    {
        return objectType == typeof(XDataTypeUid);
    }

    public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue,
        JsonSerializer serializer)
    {
        switch (reader.Value)
        {
            case string stringValue:
                return new XDataTypeUid(Guid.Parse(stringValue));
            case null when objectType == typeof(XDataTypeUid?):
                return null;
        }

        throw new NotImplementedException();
    }

    public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
    {
        var v = (XDataTypeUid)value;
        writer.WriteValue(v.Value.ToString("N"));
    }
}
