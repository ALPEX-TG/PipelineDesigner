using System;
using Newtonsoft.Json;

namespace Alpex.Measure;

public sealed class MeasureUnitJsonConverter : JsonConverter<MeasureUnit>
{
    public override MeasureUnit ReadJson(JsonReader reader, Type objectType, MeasureUnit? existingValue,
        bool hasExistingValue,
        JsonSerializer serializer)
    {
        switch (reader.Value)
        {
            case string stringValue:
                if (MeasureUnit.WellKnown.All.TryGetValue(stringValue, out var value))
                    return value;
                throw new Exception("unknown unit " + stringValue);
            case null:
                return MeasureUnit.Empty;
        }

        throw new NotImplementedException();
    }


    public override void WriteJson(JsonWriter writer, MeasureUnit? value, JsonSerializer serializer)
    {
        var unitName = value?.UnitName;
        if (string.IsNullOrEmpty(unitName))
            writer.WriteNull();
        else
        {
            if (MeasureUnit.WellKnown.All.ContainsKey(unitName))
                writer.WriteValue(unitName);
            else
                throw new NotImplementedException("Zapis jednostki " + unitName);
        }
    }
}
