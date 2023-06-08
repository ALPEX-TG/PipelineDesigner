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
public sealed class ReportImpulsAlarmMeasure
    : AbstractImpulsAlarmMeasure<ReportImpulsAlarmMeasure.Item22>
{
    public ReportImpulsAlarmMeasure(TDate date, string location, IReadOnlyList<Item22> items)
        : base(date, location,
            items)
    {
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsResistance(ImpulsAlarmMeasureKind kind)
    {
        return kind is ImpulsAlarmMeasureKind.FoamResistance or ImpulsAlarmMeasureKind.WireResistance;
    }

    public sealed class Item22 : AbstractImpulsAlarmMeasureItem
    {
        public Item22(ImpulsAlarmMeasureKind type, string loopName, SimpleMeasureValue copperWire,
            SimpleMeasureValue soldedWire, SimpleMeasureValue measureVoltage, string comments,
            SimpleColumn pipe,
            bool loopNameIsAutomatic)
            : base(type, loopName, copperWire, soldedWire, measureVoltage, comments,
                loopNameIsAutomatic)
        {
            Pipe = pipe;
        }

        public SimpleColumn Pipe { get; }
    }


    public sealed class Item22Builder : IBuilder<Item22>
    {
        public Item22Builder()
        {
        }

        public Item22Builder(Item22 source)
        {
            if (source is null) return;
            Type                = source.Type;
            LoopName            = source.LoopName;
            CopperWire          = source.CopperWire;
            SoldedWire          = source.SoldedWire;
            MeasureVoltage      = source.MeasureVoltage;
            Comments            = source.Comments;
            Pipe                = source.Pipe;
            LoopNameIsAutomatic = source.LoopNameIsAutomatic;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Item22 Build()
        {
            return new Item22(Type, LoopName, CopperWire, SoldedWire, MeasureVoltage, Comments, Pipe,
                LoopNameIsAutomatic);
        }

        public void CopyFrom(AbstractImpulsAlarmMeasureItem src)
        {
            // generator : CopyFromInterfaceGenerator.GenerateInternal:60
            Type                = src.Type;
            LoopName            = src.LoopName;
            LoopNameIsAutomatic = src.LoopNameIsAutomatic;
            CopperWire          = src.CopperWire;
            SoldedWire          = src.SoldedWire;
            MeasureVoltage      = src.MeasureVoltage;
            Comments            = src.Comments;
            // Properties count :7
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Item22Builder WithComments(string newComments)
        {
            Comments = newComments;
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Item22Builder WithCopperWire(SimpleMeasureValue newCopperWire)
        {
            CopperWire = newCopperWire;
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Item22Builder WithLoopName(string newLoopName)
        {
            LoopName = newLoopName;
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Item22Builder WithLoopNameIsAutomatic(bool newLoopNameIsAutomatic)
        {
            LoopNameIsAutomatic = newLoopNameIsAutomatic;
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Item22Builder WithMeasureVoltage(SimpleMeasureValue newMeasureVoltage)
        {
            MeasureVoltage = newMeasureVoltage;
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Item22Builder WithPipe(SimpleColumn newPipe)
        {
            Pipe = newPipe;
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Item22Builder WithSoldedWire(SimpleMeasureValue newSoldedWire)
        {
            SoldedWire = newSoldedWire;
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Item22Builder WithType(ImpulsAlarmMeasureKind newType)
        {
            Type = newType;
            return this;
        }

        public ImpulsAlarmMeasureKind Type { get; set; }

        public string LoopName { get; set; }

        public SimpleMeasureValue CopperWire { get; set; }

        public SimpleMeasureValue SoldedWire { get; set; }

        public SimpleMeasureValue MeasureVoltage { get; set; }

        public string Comments { get; set; }

        public SimpleColumn Pipe { get; set; }

        public bool LoopNameIsAutomatic { get; set; }
    }
}
