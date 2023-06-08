using System;
using Newtonsoft.Json;

namespace Alpex.Interfaces.Common;

public sealed class XAlarmContainerUidJsonConverter : JsonConverter
{
    public override bool CanConvert(Type objectType)
    {
        return objectType == typeof(XAlarmContainerUid);
    }

    public override object? ReadJson(JsonReader reader, Type objectType, object existingValue,
        JsonSerializer serializer)
    {
        return reader.Value switch
        {
            string stringValue => new XAlarmContainerUid(Guid.Parse(stringValue)),
            null when objectType == typeof(XAlarmContainerUid?) => null,
            _ => throw new NotImplementedException()
        };
    }

    public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
    {
        var v = (XAlarmContainerUid)value;
        writer.WriteValue(v.Value.ToString("N"));
    }
}
