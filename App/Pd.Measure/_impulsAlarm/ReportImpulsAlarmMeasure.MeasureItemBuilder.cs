using System.Runtime.CompilerServices;
using Pd.Interfaces.Common;

namespace Pd.Measure;

public sealed partial class ReportImpulsAlarmMeasure
{
    public sealed class MeasureItemBuilder : IBuilder<MeasureItem>, IAbstractImpulsAlarmMeasureItemBuilder
    {
        public MeasureItemBuilder()
        {
        }

        public MeasureItemBuilder(MeasureItem source)
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
        public MeasureItem Build()
        {
            return new MeasureItem(Type, LoopName, CopperWire, SoldedWire, MeasureVoltage, Comments, Pipe,
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
        public MeasureItemBuilder WithComments(string newComments)
        {
            Comments = newComments;
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public MeasureItemBuilder WithCopperWire(SimpleMeasureValue newCopperWire)
        {
            CopperWire = newCopperWire;
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public MeasureItemBuilder WithLoopName(string newLoopName)
        {
            LoopName = newLoopName;
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public MeasureItemBuilder WithLoopNameIsAutomatic(bool newLoopNameIsAutomatic)
        {
            LoopNameIsAutomatic = newLoopNameIsAutomatic;
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public MeasureItemBuilder WithMeasureVoltage(SimpleMeasureValue newMeasureVoltage)
        {
            MeasureVoltage = newMeasureVoltage;
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public MeasureItemBuilder WithPipe(SimpleColumn newPipe)
        {
            Pipe = newPipe;
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public MeasureItemBuilder WithSoldedWire(SimpleMeasureValue newSoldedWire)
        {
            SoldedWire = newSoldedWire;
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public MeasureItemBuilder WithType(ImpulsAlarmMeasureKind newType)
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
