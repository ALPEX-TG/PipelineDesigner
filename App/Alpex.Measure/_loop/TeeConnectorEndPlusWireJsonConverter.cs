using System;
using Newtonsoft.Json;

namespace Alpex.Measure;

public sealed class TeeConnectorEndPlusWireJsonConverter : JsonConverter<TeeConnectorEndPlusWire>
{
    public override TeeConnectorEndPlusWire ReadJson(JsonReader reader, Type objectType,
        TeeConnectorEndPlusWire existingValue,
        bool hasExistingValue, JsonSerializer serializer)
    {
        var st = (string)reader.Value;
        return TeeConnectorEndPlusWire.DeserializeFromString(st);
    }

    public override void WriteJson(JsonWriter writer, TeeConnectorEndPlusWire value, JsonSerializer serializer)
    {
        var serial = value.SerializeToCommaSeparatedString();
        writer.WriteValue(serial);
    }
}
