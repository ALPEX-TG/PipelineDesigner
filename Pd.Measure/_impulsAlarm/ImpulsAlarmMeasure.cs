using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Pd.Interfaces.Common;

namespace Pd.Measure;

[ImmutableObject(true)]
#if UML
[UmlNote("Data model")]
[UmlDiagram(UmlDiagrams.ViewModelsDatapointMeasureUi)]
[UmlDiagram(UmlDiagrams.ViewModelsDatapointMeasure)]
[UmlDataStructure]
#endif
[SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
public sealed partial class ImpulsAlarmMeasure : AbstractImpulsAlarmMeasure<ImpulsAlarmMeasure.Item>,
    IMeasureWithDate
{
    public ImpulsAlarmMeasure(TDate date, string location, IReadOnlyList<Item> items)
        :
        base(date, location, items)
    {
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsResistance(ImpulsAlarmMeasureKind kind)
    {
        return kind == ImpulsAlarmMeasureKind.FoamResistance || kind == ImpulsAlarmMeasureKind.WireResistance;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool NeedMeasureVoltage(ImpulsAlarmMeasureKind kind)
    {
        return IsResistance(kind);
    }

    public XDataTypeUid GetDataTypeUid()
    {
        return WellKnownCloudDataGroups.DataPointsMeasureLoopResistance;
    }

    public DateTimeOffset? GetDate()
    {
        if (Date.Value.Year < 1900)
            return null;
        return Date.Value;
    }
}
