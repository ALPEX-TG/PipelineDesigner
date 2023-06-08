using System;
using Newtonsoft.Json;

namespace Alpex.Measure;

public sealed class SimpleMeasureValueJsonConverter : JsonConverter<SimpleMeasureValue>
{
    public override SimpleMeasureValue ReadJson(JsonReader reader, Type objectType,
        SimpleMeasureValue? existingValue, bool hasExistingValue,
        JsonSerializer serializer)
    {
        switch (reader.Value)
        {
            case string str:
                return SimpleMeasureValue.Parse(str).GetValueOrThrow();
            case null:
                return SimpleMeasureValue.Empty;
        }

        throw new NotImplementedException();
    }

    public override void WriteJson(JsonWriter writer, SimpleMeasureValue? value, JsonSerializer serializer)
    {
        if (value is null || value.IsEmpty())
        {
            writer.WriteNull();
            return;
        }

        var text = value.Serialize();
        writer.WriteValue(text);
    }
}
