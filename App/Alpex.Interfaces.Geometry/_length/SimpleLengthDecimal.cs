using System;
using System.Globalization;
using Alpex.Interfaces.Common;
using Newtonsoft.Json;

namespace Alpex.Interfaces.Geometry;

[CompactSerializer]
public readonly struct SimpleLengthDecimal : ICultureFormattable, IEquatable<SimpleLengthDecimal>
{
    public SimpleLengthDecimal(decimal meters)
        : this()
    {
        Meters = meters;
    }

    public static SimpleLengthDecimal Cm(decimal value)
    {
        return new SimpleLengthDecimal(value * 0.01m);
    }

    public static SimpleLengthDecimal M(decimal value)
    {
        return new SimpleLengthDecimal(value);
    }

    public static SimpleLengthDecimal Mm(decimal value)
    {
        return new SimpleLengthDecimal(value * 0.001m);
    }

    public static SimpleLengthDecimal operator +(SimpleLengthDecimal left, SimpleLengthDecimal right)
    {
        return new SimpleLengthDecimal(left.Meters + right.Meters);
    }

    public static SimpleLengthDecimal operator /(SimpleLengthDecimal left, decimal right)
    {
        return new SimpleLengthDecimal(left.Meters / right);
    }

    public static bool operator ==(SimpleLengthDecimal left, SimpleLengthDecimal right)
    {
        return Equals(left, right);
    }

    public static explicit operator SimpleLengthDecimal(Length x)
    {
        return new SimpleLengthDecimal(x.GetMeters());
    }

    public static explicit operator Length(SimpleLengthDecimal x)
    {
        return Length.FromMeter(x.Meters);
    }

    public static bool operator !=(SimpleLengthDecimal left, SimpleLengthDecimal right)
    {
        return !Equals(left, right);
    }

    public static SimpleLengthDecimal operator *(SimpleLengthDecimal left, decimal right)
    {
        return new SimpleLengthDecimal(left.Meters * right);
    }

    public static SimpleLengthDecimal operator *(decimal left, SimpleLengthDecimal right)
    {
        return new SimpleLengthDecimal(right.Meters * left);
    }

    public static SimpleLengthDecimal operator -(SimpleLengthDecimal left, SimpleLengthDecimal right)
    {
        return new SimpleLengthDecimal(left.Meters - right.Meters);
    }

    public static SimpleLengthDecimal operator -(SimpleLengthDecimal x)
    {
        return new SimpleLengthDecimal(-x.Meters);
    }

    public override bool Equals(object?  other)
    {
        if (other is null) return false;
        if (other.GetType() != typeof(SimpleLengthDecimal)) return false;
        return Equals((SimpleLengthDecimal)other);
    }

    public bool Equals(SimpleLengthDecimal other)
    {
        return Meters.Equals(other.Meters);
    }

    public override int GetHashCode()
    {
        return Meters.GetHashCode();
    }

    public SimpleLength ToSimpleLength()
    {
        return new SimpleLength((double)Meters);
    }

    public override string ToString()
    {
        return ToString(null);
    }

    public string ToString(IFormatProvider formatProvider)
    {
        return Meters.ToString(formatProvider ?? CultureInfo.CurrentCulture) + " m";
    }

    public decimal Meters { get; }

    [JsonIgnore]
    public decimal MiliMeters => Meters * 1000;
}
