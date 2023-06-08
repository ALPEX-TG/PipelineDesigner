using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;

namespace Pd.Measure;

[JsonConverter(typeof(MeasureUnitJsonConverter))]
[ImmutableObject(true)]
public sealed partial class MeasureUnit : IEquatable<MeasureUnit>
{
    public MeasureUnit(MeasuredValue type, string? unitName, decimal factor)
    {
        Type     = type;
        UnitName = unitName ?? string.Empty;
        Factor   = factor;
    }

    public static bool operator ==(MeasureUnit left, MeasureUnit right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(MeasureUnit left, MeasureUnit right)
    {
        return !Equals(left, right);
    }

    public override bool Equals(object? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return other is MeasureUnit otherCasted && Equals(otherCasted);
    }

    public bool Equals(MeasureUnit? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        if (IsEmpty) return other.IsEmpty;
        if (other.IsEmpty) return false;
        return Type == other.Type
               && Factor.Equals(other.Factor)
               && StringComparer.Ordinal.Equals(UnitName, other.UnitName);
    }

    public override int GetHashCode()
    {
        if (IsEmpty) return 0;
        unchecked
        {
            return (StringComparer.Ordinal.GetHashCode(UnitName) * 397 ^ Factor.GetHashCode()) * 2 +
                   (int)Type;
        }
    }

    public override string ToString()
    {
        return UnitName;
    }

    public static MeasureUnit Empty => new(MeasuredValue.Voltage, string.Empty, 1);

    public MeasuredValue Type     { get; }
    public string        UnitName { get; }
    public decimal       Factor   { get; }

    [JsonIgnore]
    public bool IsEmpty
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => string.IsNullOrEmpty(UnitName);
    }

    public static partial class WellKnown
    {
        static WellKnown()
        {
            All = GetDictionary();
        }

        public static readonly IReadOnlyDictionary<string, MeasureUnit> All;
    }
}
