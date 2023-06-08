using System;
using System.Runtime.CompilerServices;

namespace Alpex.Interfaces.Geometry;

public readonly partial struct AngleDeg
{
    public static AngleDeg operator +(AngleDeg left, DiffAngleDeg right)
    {
        return new AngleDeg(left.Degrees + right.Degrees);
    }

    public static AngleDeg operator +(DiffAngleDeg left, AngleDeg right)
    {
        return new AngleDeg(left.Degrees + right.Degrees);
    }

    public static explicit operator MinuteAngle(AngleDeg a)
    {
        var minutes = a.GetMinutesAngleRaw();
        return new MinuteAngle((int)Math.Round(minutes));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static explicit operator Radians(AngleDeg a)
    {
        return new Radians(a.RadiansAsDouble);
    }

    public static implicit operator AngleDeg(MinuteAngle a)
    {
        var degrees = 90 - a.Minutes * 6;
        return new AngleDeg(degrees);
    }


    public static DiffAngleDeg operator -(AngleDeg left, AngleDeg right)
    {
        return new DiffAngleDeg(left.Degrees - right.Degrees);
    }

    public string GetHourAnglePrintable()
    {
        var hour    = GetHourAngleRaw();
        var hourInt = (int)Math.Round(hour);
        if (hourInt == 0)
            hourInt = 12;
        return $"{hourInt}'";
    }

    public decimal GetHourAngleRaw()
    {
        var hours = (90 - Degrees) / (6 * 5);
        if (hours < 0)
            hours += 12;
        return hours;
    }

    public decimal GetMinutesAngleRaw()
    {
        var minutes = (90 - Degrees) / 6;
        return minutes;
    }
}

public readonly partial struct DiffAngleDeg
{
    public static DiffAngleDeg operator +(DiffAngleDeg left, DiffAngleDeg right)
    {
        return new DiffAngleDeg(left.Degrees + right.Degrees);
    }

    public static explicit operator DiffMinuteAngle(DiffAngleDeg a)
    {
        var minutes = -a.Degrees / 6;
        return new DiffMinuteAngle((int)Math.Round(minutes));
    }

    public static implicit operator DiffAngleDeg(DiffMinuteAngle a)
    {
        var degrees = -a.Minutes * 6;
        return new DiffAngleDeg(degrees);
    }
}

public readonly partial struct MinuteAngle
{
    public static MinuteAngle operator +(MinuteAngle left, DiffMinuteAngle right)
    {
        return new MinuteAngle(left.Minutes + right.Minutes);
    }

    public static MinuteAngle operator +(DiffMinuteAngle left, MinuteAngle right)
    {
        return new MinuteAngle(left.Minutes + right.Minutes);
    }

    public static DiffMinuteAngle operator -(MinuteAngle a, MinuteAngle b)
    {
        return new DiffMinuteAngle(a.Minutes - b.Minutes);
    }
}

public readonly partial struct DiffMinuteAngle
{
    public static DiffMinuteAngle operator +(DiffMinuteAngle left, DiffMinuteAngle right)
    {
        return new DiffMinuteAngle(left.Minutes + right.Minutes);
    }
}
