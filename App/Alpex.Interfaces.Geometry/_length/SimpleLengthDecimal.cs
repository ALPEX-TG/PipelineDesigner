using System;
using System.Globalization;
using Alpex.Interfaces.Common;
using Newtonsoft.Json;

namespace Alpex.Interfaces.Geometry;

[CompactSerializer]
public readonly partial struct SimpleLengthDecimal : ICultureFormattable, IEquatable<SimpleLengthDecimal>, IFormattable
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

    public static explicit operator SimpleLengthDecimal(Length x)
    {
        return new SimpleLengthDecimal(x.GetMeters());
    }

    public static explicit operator Length(SimpleLengthDecimal x)
    {
        return Length.FromMeter(x.Meters);
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

    public string ToString(string? format, IFormatProvider? formatProvider)
    {
        return Meters.ToString(format, formatProvider ?? CultureInfo.CurrentCulture) + " m";
    }

    public string ToString(IFormatProvider? formatProvider)
    {
        return Meters.ToString(formatProvider ?? CultureInfo.CurrentCulture) + " m";
    }

    public decimal Meters { get; }

    [JsonIgnore]
    public decimal MiliMeters => Meters * 1000;
}
