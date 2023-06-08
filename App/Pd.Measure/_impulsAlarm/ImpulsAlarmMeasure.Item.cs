using System;
using System.ComponentModel;

namespace Pd.Measure;

public sealed partial class ImpulsAlarmMeasure
{
    [ImmutableObject(true)]
#if UML
    [UmlDiagram(UmlDiagrams.ViewModelsDatapointMeasureUi)]
    [UmlDiagram(UmlDiagrams.ViewModelsDatapointMeasure)]
    [UmlDataStructure]
#endif
    public sealed class Item : AbstractImpulsAlarmMeasureItem, IEquatable<Item>
    {
        public Item(
            ImpulsAlarmMeasureKind type,
            SimpleMeasureValue copperWire,
            SimpleMeasureValue soldedWire,
            SimpleMeasureValue measureVoltage,
            string? comments,
            VtCompactLoopEndReference copperWireReference,
            VtCompactLoopEndReference soldedWireReference,
            string loopName,
            bool loopNameIsAutomatic
        )
            : base(
                type, loopName, copperWire, soldedWire, measureVoltage, comments, loopNameIsAutomatic)
        {
            CopperWireReference = copperWireReference;
            SoldedWireReference = soldedWireReference;
        }

        public VtCompactLoopEndReference CopperWireReference { get; }

        public VtCompactLoopEndReference SoldedWireReference { get; }

        public override bool Equals(object? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            return other is Item otherCasted && Equals(otherCasted);
        }

        public bool Equals(Item? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            return LoopNameIsAutomatic == other.LoopNameIsAutomatic
                   && Type == other.Type
                   && StringComparer.Ordinal.Equals(LoopName, other.LoopName)
                   && StringComparer.Ordinal.Equals(Comments ?? string.Empty, other.Comments ?? string.Empty)
                   && CopperWireReference.Equals(other.CopperWireReference)
                   && SoldedWireReference.Equals(other.SoldedWireReference)
                   && Equals(CopperWire, other.CopperWire)
                   && Equals(SoldedWire, other.SoldedWire)
                   && Equals(MeasureVoltage, other.MeasureVoltage);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var code = CopperWireReference.GetHashCode() * 397 ^ SoldedWireReference.GetHashCode();
                code = code * 397 ^ StringComparer.Ordinal.GetHashCode(LoopName ?? string.Empty);
                code = (code * 397 ^ (CopperWire?.GetHashCode() ?? 0)) * 397 ^ (SoldedWire?.GetHashCode() ?? 0);
                code = code * 397 ^ (MeasureVoltage?.GetHashCode() ?? 0);
                code = (code * 397 ^ StringComparer.Ordinal.GetHashCode(Comments ?? string.Empty)) * 3 + (int)Type;
                return code * 2 + (LoopNameIsAutomatic ? 1 : 0);
            }
        }

        public bool ShouldSerializeCopperWireReference()
        {
            return CopperWireReference.HasAnyValue();
        }

        public bool ShouldSerializeSoldedWireReference()
        {
            return SoldedWireReference.HasAnyValue();
        }

        public static bool operator !=(Item left, Item right)
        {
            return !Equals(left, right);
        }

        public static bool operator ==(Item left, Item right)
        {
            return Equals(left, right);
        }
    }
}
