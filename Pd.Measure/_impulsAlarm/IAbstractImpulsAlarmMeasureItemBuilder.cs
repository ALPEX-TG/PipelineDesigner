namespace Pd.Measure;

public interface IAbstractImpulsAlarmMeasureItemBuilder
{
    ImpulsAlarmMeasureKind Type { get; set; }

    string LoopName { get; set; }

    SimpleMeasureValue CopperWire { get; set; }

    SimpleMeasureValue SoldedWire { get; set; }

    SimpleMeasureValue MeasureVoltage { get; set; }

    string? Comments { get; set; }

    bool LoopNameIsAutomatic { get; set; }
}
