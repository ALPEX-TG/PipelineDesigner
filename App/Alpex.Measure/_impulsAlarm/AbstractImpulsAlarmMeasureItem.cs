using Alpex.Interfaces.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Alpex.Measure;

public class AbstractImpulsAlarmMeasureItem
{
    public AbstractImpulsAlarmMeasureItem(ImpulsAlarmMeasureKind type,
        string? loopName,
        SimpleMeasureValue copperWire,
        SimpleMeasureValue soldedWire,
        SimpleMeasureValue measureVoltage,
        string? comments,
        bool loopNameIsAutomatic)
    {
        Type                = type;
        LoopName            = loopName ?? "";
        CopperWire          = copperWire;
        SoldedWire          = soldedWire;
        MeasureVoltage      = measureVoltage;
        Comments            = comments;
        LoopNameIsAutomatic = loopNameIsAutomatic;
    }

    public bool ShouldSerializeComments()
    {
        return !string.IsNullOrEmpty(Comments);
    }

    public bool ShouldSerializeCopperWire()
    {
        return CopperWire != null && !CopperWire.IsEmpty();
    }

    public bool ShouldSerializeMeasureVoltage()
    {
        return MeasureVoltage != null && !MeasureVoltage.IsEmpty();
    }

    public bool ShouldSerializeSoldedWire()
    {
        return SoldedWire != null && !SoldedWire.IsEmpty();
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public ImpulsAlarmMeasureKind Type { get; }

    public string LoopName { get; }

    public bool LoopNameIsAutomatic { get; }

    public SimpleMeasureValue CopperWire { get; }

    public SimpleMeasureValue SoldedWire { get; }

    public SimpleMeasureValue MeasureVoltage { get; }

    [NullToStringEmpty]
    public string? Comments { get; }
}
