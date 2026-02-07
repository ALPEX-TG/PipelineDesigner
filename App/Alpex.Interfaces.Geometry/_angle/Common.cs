using System;

namespace Alpex.Interfaces.Geometry;
partial struct AngleDeg : IComparable<AngleDeg>, IComparable
{
    public int CompareTo(AngleDeg other) => Degrees.CompareTo(other.Degrees);

    public int CompareTo(object? obj)
    {
        if (obj is null) return 1;
        return obj is AngleDeg other ? CompareTo(other) : throw new ArgumentException($"Object must be of type {nameof(AngleDeg)}");
    }

    public static bool operator <(AngleDeg left, AngleDeg right)
        => left.CompareTo(right) < 0;

    public static bool operator >(AngleDeg left, AngleDeg right)
        => left.CompareTo(right) > 0;

    public static bool operator <=(AngleDeg left, AngleDeg right)
        => left.CompareTo(right) <= 0;

    public static bool operator >=(AngleDeg left, AngleDeg right)
        => left.CompareTo(right) >= 0;

    public override bool Equals(object? other)
    {
        if (other is null) return false;
        if (other.GetType() != typeof(AngleDeg)) return false;
        return Equals((AngleDeg)other);
    }

    public bool Equals(AngleDeg other)
    {
        return Degrees.Equals(other.Degrees);
    }

    public static bool operator ==(AngleDeg left, AngleDeg right)
        => Equals(left, right);

    public static bool operator !=(AngleDeg left, AngleDeg right)
        => !Equals(left, right);

}

partial struct MinuteAngle : IComparable<MinuteAngle>, IComparable
{
    public int CompareTo(MinuteAngle other) => Minutes.CompareTo(other.Minutes);

    public int CompareTo(object? obj)
    {
        if (obj is null) return 1;
        return obj is MinuteAngle other ? CompareTo(other) : throw new ArgumentException($"Object must be of type {nameof(MinuteAngle)}");
    }

    public static bool operator <(MinuteAngle left, MinuteAngle right)
        => left.CompareTo(right) < 0;

    public static bool operator >(MinuteAngle left, MinuteAngle right)
        => left.CompareTo(right) > 0;

    public static bool operator <=(MinuteAngle left, MinuteAngle right)
        => left.CompareTo(right) <= 0;

    public static bool operator >=(MinuteAngle left, MinuteAngle right)
        => left.CompareTo(right) >= 0;

    public override bool Equals(object? other)
    {
        if (other is null) return false;
        if (other.GetType() != typeof(MinuteAngle)) return false;
        return Equals((MinuteAngle)other);
    }

    public bool Equals(MinuteAngle other)
    {
        return Minutes.Equals(other.Minutes);
    }

    public static bool operator ==(MinuteAngle left, MinuteAngle right)
        => Equals(left, right);

    public static bool operator !=(MinuteAngle left, MinuteAngle right)
        => !Equals(left, right);

}

partial struct Radians : IComparable<Radians>, IComparable
{
    public int CompareTo(Radians other) => Value.CompareTo(other.Value);

    public int CompareTo(object? obj)
    {
        if (obj is null) return 1;
        return obj is Radians other ? CompareTo(other) : throw new ArgumentException($"Object must be of type {nameof(Radians)}");
    }

    public static bool operator <(Radians left, Radians right)
        => left.CompareTo(right) < 0;

    public static bool operator >(Radians left, Radians right)
        => left.CompareTo(right) > 0;

    public static bool operator <=(Radians left, Radians right)
        => left.CompareTo(right) <= 0;

    public static bool operator >=(Radians left, Radians right)
        => left.CompareTo(right) >= 0;

    public override bool Equals(object? other)
    {
        if (other is null) return false;
        if (other.GetType() != typeof(Radians)) return false;
        return Equals((Radians)other);
    }

    public bool Equals(Radians other)
    {
        return Value.Equals(other.Value);
    }

    public static bool operator ==(Radians left, Radians right)
        => Equals(left, right);

    public static bool operator !=(Radians left, Radians right)
        => !Equals(left, right);

}

