using System;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using Alpex.Interfaces.Common;
using Newtonsoft.Json;

namespace Alpex.Interfaces.Geometry;

[CompactSerializer]
[TypeConverter(typeof(SimpleLengthTypeConverter))]
[JsonConverter(typeof(SimpleLengthJsonConverter))]
public readonly struct SimpleLength : ICultureFormattable, IFormattable, IEquatable<SimpleLength>
{
    internal SimpleLength(decimal meters)
    {
        Meters = (double)meters;
    }

    private SimpleLength(decimal value, decimal factor)
    {
        Meters = (double)(factor * value);
    }

    private SimpleLength(double value, decimal factor)
    {
        Meters = (double)(factor * (decimal)value);
    }

    [JsonConstructor]
    public SimpleLength(double meters)
    {
        Meters = meters;
    }

    public static SimpleLength Cm(double value)
    {
        return new SimpleLength(value, 0.01m);
    }

    public static SimpleLength Cm(decimal value)
    {
        return new SimpleLength(value, 0.01m);
    }

    public static SimpleLength Cm(int value)
    {
        return new SimpleLength((decimal)value, 0.01m);
    }

    public static SimpleLength Cm(long value)
    {
        return new SimpleLength((decimal)value, 0.01m);
    }

    public static SimpleLength? Cm(double? value)
    {
        return value is null ? null : Cm(value.Value);
    }

    public static SimpleLength M(double value)
    {
        return new SimpleLength(value);
    }

    public static SimpleLength? M(double? value)
    {
        return value is null ? null : M(value.Value);
    }

    public static SimpleLength Mm(double value)
    {
        return new SimpleLength(value, 0.001m);
    }

    public static SimpleLength? Mm(double? value)
    {
        return value is null ? null : Mm(value.Value);
    }

    public static SimpleLength Mm(int value)
    {
        return new SimpleLength((decimal)value, 0.001m);
    }

    public static SimpleLength Mm(long value)
    {
        return new SimpleLength((decimal)value, 0.001m);
    }

    public static SimpleLength Mm(decimal value)
    {
        return new SimpleLength(value, 0.001m);
    }

    public static SimpleLength operator +(SimpleLength left, SimpleLength right)
    {
        return new SimpleLength(left.Meters + right.Meters);
    }

    public static SimpleLength operator /(SimpleLength left, double right)
    {
        return new SimpleLength(left.Meters / right);
    }

    public static bool operator ==(SimpleLength left, SimpleLength right)
    {
        return Equals(left, right);
    }

    public static explicit operator Length(SimpleLength l)
    {
        return Length.FromMeter((decimal)l.Meters);
    }

    public static bool operator !=(SimpleLength left, SimpleLength right)
    {
        return !Equals(left, right);
    }

    public static SimpleLength operator *(SimpleLength left, double right)
    {
        return new SimpleLength(left.Meters * right);
    }

    public static SimpleLength operator *(double left, SimpleLength right)
    {
        return new SimpleLength(right.Meters * left);
    }

    public static SimpleLength operator -(SimpleLength left, SimpleLength right)
    {
        return new SimpleLength(left.Meters - right.Meters);
    }

    public static SimpleLength operator -(SimpleLength x)
    {
        return new SimpleLength(-x.Meters);
    }

    public override bool Equals(object? other)
    {
        if (other is null) return false;
        if (other.GetType() != typeof(SimpleLength)) return false;
        return Equals((SimpleLength)other);
    }

    public bool Equals(SimpleLength other)
    {
        return Meters.Equals(other.Meters);
    }

    public override int GetHashCode()
    {
        return Meters.GetHashCode();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsZero()
    {
        return Meters.Equals(0d);
    }

    public override string ToString()
    {
        return ToString(null);
    }

    public string ToString(string? format, IFormatProvider? formatProvider)
    {
        formatProvider ??= CultureInfo.CurrentCulture;
        var t = UnitedValuesFormatProvider.TryParse(format);
        if (t is null)
            return ToString(formatProvider);
        var s = t.Format(Meters, LengthUnits.Meter, formatProvider);
        return s;
    }

    public string ToString(IFormatProvider? formatProvider)
    {
        return Meters.ToString(formatProvider ?? CultureInfo.CurrentCulture) + " " + LengthUnits.Meter;
    }

    public static SimpleLength Zero => new SimpleLength(0d);

    public double Meters
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get;
    }

    public double MiliMeters => Meters * 1000;
}
