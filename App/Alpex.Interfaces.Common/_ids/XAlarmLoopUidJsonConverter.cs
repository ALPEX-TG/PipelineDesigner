using System;
using Newtonsoft.Json;

namespace Alpex.Interfaces.Common;

public sealed class XAlarmLoopUidJsonConverter : JsonConverter
{
    public override bool CanConvert(Type objectType)
    {
        return objectType == typeof(XAlarmLoopUid);
    }

    public override object? ReadJson(JsonReader reader, Type objectType, object existingValue,
        JsonSerializer serializer)
    {
        return reader.Value switch
        {
            string stringValue => new XAlarmLoopUid(Guid.Parse(stringValue)),
            null when objectType == typeof(XAlarmLoopUid?) => null,
            _ => throw new NotImplementedException()
        };
    }

    public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
    {
        var v = (XAlarmLoopUid)value;
        writer.WriteValue(v.Value.ToString("N"));
    }
}
