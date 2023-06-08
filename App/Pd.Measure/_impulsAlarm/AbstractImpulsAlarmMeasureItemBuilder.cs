using System.Runtime.CompilerServices;
using Pd.Interfaces.Common;

namespace Pd.Measure;

public class AbstractImpulsAlarmMeasureItemBuilder : IBuilder<AbstractImpulsAlarmMeasureItem>,
    IAbstractImpulsAlarmMeasureItemBuilder
{
    public AbstractImpulsAlarmMeasureItemBuilder()
    {
    }

    public AbstractImpulsAlarmMeasureItemBuilder(AbstractImpulsAlarmMeasureItem source)
    {
        if (source is null) return;
        Type                = source.Type;
        LoopName            = source.LoopName;
        CopperWire          = source.CopperWire;
        SoldedWire          = source.SoldedWire;
        MeasureVoltage      = source.MeasureVoltage;
        Comments            = source.Comments;
        LoopNameIsAutomatic = source.LoopNameIsAutomatic;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public AbstractImpulsAlarmMeasureItem Build()
    {
        return new AbstractImpulsAlarmMeasureItem(Type, LoopName, CopperWire, SoldedWire, MeasureVoltage, Comments,
            LoopNameIsAutomatic);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public AbstractImpulsAlarmMeasureItemBuilder WithComments(string? newComments)
    {
        Comments = newComments;
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public AbstractImpulsAlarmMeasureItemBuilder WithCopperWire(SimpleMeasureValue newCopperWire)
    {
        CopperWire = newCopperWire;
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public AbstractImpulsAlarmMeasureItemBuilder WithLoopName(string newLoopName)
    {
        LoopName = newLoopName;
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public AbstractImpulsAlarmMeasureItemBuilder WithLoopNameIsAutomatic(bool newLoopNameIsAutomatic)
    {
        LoopNameIsAutomatic = newLoopNameIsAutomatic;
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public AbstractImpulsAlarmMeasureItemBuilder WithMeasureVoltage(SimpleMeasureValue newMeasureVoltage)
    {
        MeasureVoltage = newMeasureVoltage;
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public AbstractImpulsAlarmMeasureItemBuilder WithSoldedWire(SimpleMeasureValue newSoldedWire)
    {
        SoldedWire = newSoldedWire;
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public AbstractImpulsAlarmMeasureItemBuilder WithType(ImpulsAlarmMeasureKind newType)
    {
        Type = newType;
        return this;
    }

    public ImpulsAlarmMeasureKind Type { get; set; }

    public string LoopName { get; set; }

    public SimpleMeasureValue CopperWire { get; set; }

    public SimpleMeasureValue SoldedWire { get; set; }

    public SimpleMeasureValue MeasureVoltage { get; set; }

    public string? Comments { get; set; }

    public bool LoopNameIsAutomatic { get; set; }
}
