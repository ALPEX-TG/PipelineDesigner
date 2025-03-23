using System.Runtime.CompilerServices;
using Alpex.Interfaces.Common;

namespace Alpex.Measure;

public sealed partial class ImpulsAlarmMeasure
{
    public sealed class ItemBuilder : IBuilder<Item>, IAbstractImpulsAlarmMeasureItemBuilder
    {
        public ItemBuilder()
        {
        }

        public ItemBuilder(Item? source)
        {
            if (source is null) return;
            Type                = source.Type;
            CopperWire          = source.CopperWire;
            SoldedWire          = source.SoldedWire;
            MeasureVoltage      = source.MeasureVoltage;
            Comments            = source.Comments;
            CopperWireReference = source.CopperWireReference;
            SoldedWireReference = source.SoldedWireReference;
            LoopName            = source.LoopName;
            LoopNameIsAutomatic = source.LoopNameIsAutomatic;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Item Build()
        {
            return new Item(Type, CopperWire, SoldedWire, MeasureVoltage, Comments, CopperWireReference,
                SoldedWireReference, LoopName, LoopNameIsAutomatic);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ItemBuilder WithComments(string? newComments)
        {
            Comments = newComments;
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ItemBuilder WithCopperWire(SimpleMeasureValue newCopperWire)
        {
            CopperWire = newCopperWire;
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ItemBuilder WithCopperWireReference(VtCompactLoopEndReference newCopperWireReference)
        {
            CopperWireReference = newCopperWireReference;
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ItemBuilder WithLoopName(string newLoopName)
        {
            LoopName = newLoopName;
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ItemBuilder WithLoopNameIsAutomatic(bool newLoopNameIsAutomatic)
        {
            LoopNameIsAutomatic = newLoopNameIsAutomatic;
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ItemBuilder WithMeasureVoltage(SimpleMeasureValue newMeasureVoltage)
        {
            MeasureVoltage = newMeasureVoltage;
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ItemBuilder WithSoldedWire(SimpleMeasureValue newSoldedWire)
        {
            SoldedWire = newSoldedWire;
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ItemBuilder WithSoldedWireReference(VtCompactLoopEndReference newSoldedWireReference)
        {
            SoldedWireReference = newSoldedWireReference;
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ItemBuilder WithType(ImpulsAlarmMeasureKind newType)
        {
            Type = newType;
            return this;
        }

        public VtCompactLoopEndReference CopperWireReference { get; set; }

        public VtCompactLoopEndReference SoldedWireReference { get; set; }

        public ImpulsAlarmMeasureKind Type { get; set; }

        public SimpleMeasureValue CopperWire { get; set; }

        public SimpleMeasureValue SoldedWire { get; set; }

        public SimpleMeasureValue MeasureVoltage { get; set; }

        public string? Comments { get; set; }

        public string LoopName { get; set; }

        public bool LoopNameIsAutomatic { get; set; }
    }
}
