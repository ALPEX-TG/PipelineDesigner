using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Pd.Interfaces.Common;

namespace Pd.Measure;

/// <summary>
///     To samo, co <see cref="ImpulsAlarmMeasure">ImpulsAlarmMeasure</see> ale zawiera pełne referencje do
///     zakończeń drutu, a nie krótkie.
///     W trakcie edycji uzyskanie krótkich referencji jest trudne
/// </summary>
[ImmutableObject(true)]
[SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
public sealed partial class ReportImpulsAlarmMeasure
    : AbstractImpulsAlarmMeasure<ReportImpulsAlarmMeasure.MeasureItem>
{
    public ReportImpulsAlarmMeasure(TDate date, string location, IReadOnlyList<MeasureItem> items)
        : base(date, location,
            items)
    {
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsResistance(ImpulsAlarmMeasureKind kind)
    {
        return kind is ImpulsAlarmMeasureKind.FoamResistance or ImpulsAlarmMeasureKind.WireResistance;
    }
}
