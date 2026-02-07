using System;
using iSukces.Mathematics;
using Newtonsoft.Json;

namespace Alpex.Interfaces.Geometry;

public readonly partial struct Radians
{
    public Radians(double radians)
    {
        Value = radians % (2 * Math.PI);
        if (Value < 0)
            Value = 2 * Math.PI + Value;
    }

    public static Radians FromDegrees(double degrees)
    {
        return new Radians(degrees * Math.PI / 180.0);
    }

    public static Radians operator +(Radians left, Radians right)
    {
        return new Radians(left.Value + right.Value);
    }

    public static Radians operator /(Radians r, double factor)
    {
        return new Radians(r.Value / factor);
    }

    public static implicit operator Radians(double radians)
    {
        return new Radians(radians);
    }

    public static Radians operator *(Radians r, double factor)
    {
        return new Radians(r.Value * factor);
    }

    public static Radians operator -(Radians left, Radians right)
    {
        return new Radians(left.Value - right.Value);
    }

    public static Radians operator -(Radians r)
    {
        return new Radians(-r.Value);
    }

    public double Cos()
    {
        return Math.Cos(Value);
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }

    public double Sin()
    {
        return Math.Sin(Value);
    }

    public double ToDegrees()
    {
        return Value * (180.0 / Math.PI);
    }

    public override string ToString()
    {
        return ToDegrees().ToString("F1") + "°";
    }

    [JsonIgnore]
    public SinusCosinus Tri => SinusCosinus.FromAngleRad(Value);

    [JsonIgnore]
    public int QuarterIndex => (int)(2.0 * Value / Math.PI);

    [JsonIgnore]
    public int OctaveIndex => (int)(4.0 * Value / Math.PI);

    public double Value { get; }

    public static readonly Radians Zero = new Radians();
    public static readonly Radians Deg45 = new Radians(Math.PI * 0.25);
    public static readonly Radians Deg90 = new Radians(Math.PI * 0.5);
    public static readonly Radians Deg180 = new Radians(Math.PI);
}
