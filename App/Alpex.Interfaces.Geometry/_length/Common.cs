using System;

namespace Alpex.Interfaces.Geometry;
partial struct SimpleLength : IComparable<SimpleLength>, IEquatable<SimpleLength>, IComparable
{
    public int CompareTo(SimpleLength other) => Meters.CompareTo(other.Meters);

    public int CompareTo(object? obj)
    {
        if (obj is null) return 1;
        return obj is SimpleLength other ? CompareTo(other) : throw new ArgumentException($"Object must be of type {nameof(SimpleLength)}");
    }

    public static bool operator <(SimpleLength left, SimpleLength right)
        => left.CompareTo(right) < 0;

    public static bool operator >(SimpleLength left, SimpleLength right)
        => left.CompareTo(right) > 0;

    public static bool operator <=(SimpleLength left, SimpleLength right)
        => left.CompareTo(right) <= 0;

    public static bool operator >=(SimpleLength left, SimpleLength right)
        => left.CompareTo(right) >= 0;

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

    public static bool operator ==(SimpleLength left, SimpleLength right)
        => Equals(left, right);

    public static bool operator !=(SimpleLength left, SimpleLength right)
        => !Equals(left, right);

}

partial struct SimpleLengthDecimal : IComparable<SimpleLengthDecimal>, IEquatable<SimpleLengthDecimal>, IComparable
{
    public int CompareTo(SimpleLengthDecimal other) => Meters.CompareTo(other.Meters);

    public int CompareTo(object? obj)
    {
        if (obj is null) return 1;
        return obj is SimpleLengthDecimal other ? CompareTo(other) : throw new ArgumentException($"Object must be of type {nameof(SimpleLengthDecimal)}");
    }

    public static bool operator <(SimpleLengthDecimal left, SimpleLengthDecimal right)
        => left.CompareTo(right) < 0;

    public static bool operator >(SimpleLengthDecimal left, SimpleLengthDecimal right)
        => left.CompareTo(right) > 0;

    public static bool operator <=(SimpleLengthDecimal left, SimpleLengthDecimal right)
        => left.CompareTo(right) <= 0;

    public static bool operator >=(SimpleLengthDecimal left, SimpleLengthDecimal right)
        => left.CompareTo(right) >= 0;

    public override bool Equals(object? other)
    {
        if (other is null) return false;
        if (other.GetType() != typeof(SimpleLengthDecimal)) return false;
        return Equals((SimpleLengthDecimal)other);
    }

    public bool Equals(SimpleLengthDecimal other)
    {
        return Meters.Equals(other.Meters);
    }

    public static bool operator ==(SimpleLengthDecimal left, SimpleLengthDecimal right)
        => Equals(left, right);

    public static bool operator !=(SimpleLengthDecimal left, SimpleLengthDecimal right)
        => !Equals(left, right);

}

partial struct Length : IComparable<Length>, IEquatable<Length>, IComparable
{
    public int CompareTo(Length other) => GetMeters().CompareTo(other.GetMeters());

    public int CompareTo(object? obj)
    {
        if (obj is null) return 1;
        return obj is Length other ? CompareTo(other) : throw new ArgumentException($"Object must be of type {nameof(Length)}");
    }

    public static bool operator <(Length left, Length right)
        => left.CompareTo(right) < 0;

    public static bool operator >(Length left, Length right)
        => left.CompareTo(right) > 0;

    public static bool operator <=(Length left, Length right)
        => left.CompareTo(right) <= 0;

    public static bool operator >=(Length left, Length right)
        => left.CompareTo(right) >= 0;

}

