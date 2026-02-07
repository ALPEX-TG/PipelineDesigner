using System;
using System.ComponentModel;
using System.Globalization;
using System.Text.RegularExpressions;
using Alpex.Interfaces.Common;
using iSukces.Mathematics;
using Newtonsoft.Json;

namespace Alpex.Interfaces.Geometry;

[CompactSerializer]
[TypeConverter(typeof(AngleDegTypeConverter))]
[JsonConverter(typeof(AngleDegJsonConverter))]
public readonly partial struct AngleDeg : IEquatable<AngleDeg>, IComparable<AngleDeg>
{
    [JsonConstructor]
    public AngleDeg(decimal degrees)
    {
        Degrees = NormalizeAngleDeg(degrees);
    }

    public AngleDeg(int degrees)
    {
        Degrees = NormalizeAngleDeg(degrees);
    }

    public AngleDeg(double degrees)
    {
        Degrees = NormalizeAngleDeg((decimal)degrees);
    }

    public static decimal AngleBetween(decimal a, decimal b)
    {
        var dif = Math.Abs(a - b);
        dif = Normalize0360(dif);
        if (dif > 180)
            dif = 360 - dif;
        return dif;
    }

    public static string? IsValidAngleDegString(object? value, CultureInfo cultureInfo)
    {
        return Parse(value as string ?? "", cultureInfo.NumberFormat).Error;
    }

    public static double Normalize0180(double angle)
    {
        const double full = 180;
        if (angle >= 0 && angle < full)
            return angle;
        return angle - Math.Floor(angle / full) * full;
    }

    public static double Normalize0360(double angle)
    {
        const double full = 360;
        if (angle >= 0 && angle < full)
            return angle;
        return angle - Math.Floor(angle / full) * full;
    }

    public static decimal Normalize0360(decimal angle)
    {
        const decimal full = 360;
        if (angle >= 0 && angle < full)
            return angle;
        return angle - Math.Floor(angle / full) * full;
    }


    private static decimal NormalizeAngleDeg(decimal angle)
    {
        return angle is < 0 or >= 360 ? angle - Math.Floor(angle / 360) * 360 : angle;
    }


    public static AngleDeg operator /(AngleDeg angle, decimal number)
    {
        return new AngleDeg(angle.Degrees / number);
    }

    public static explicit operator AngleDeg(decimal x)
    {
        return new AngleDeg(x);
    }

    public static explicit operator decimal(AngleDeg x)
    {
        return x.Degrees;
    }

    public static implicit operator SinusCosinus(AngleDeg x)
    {
        if (x.Degrees == 0m)
            return new SinusCosinus(0, 1);
        if (x.Degrees == 180m)
            return new SinusCosinus(0, -1);
        if (x.Degrees == 90m)
            return new SinusCosinus(1, 0);
        if (x.Degrees == 270m)
            return new SinusCosinus(-1, 0);
        return SinusCosinus.FromAngleDeg((double)x.Degrees);
    }

    public static AngleDeg operator *(AngleDeg angle, decimal number)
    {
        return new AngleDeg(angle.Degrees * number);
    }

    public static AngleDeg operator *(decimal number, AngleDeg angle)
    {
        return new AngleDeg(angle.Degrees * number);
    }

    public static ParseResult<AngleDeg> Parse(string? x, NumberFormatInfo fi)
    {
        x = Length.ProcessNumber(x, fi);
        if (string.IsNullOrEmpty(x))
            return ParseResult<AngleDeg>.NotOk(ValidationBase.NotEmpty);
        var m = Re.Match(x);
        if (!m.Success)
            return ParseResult<AngleDeg>.NotOk($"wartość '{x}' nie jest prawidłowym oznaczenim kąta");
        var value = decimal.Parse(m.Groups[1].Value, CultureInfo.InvariantCulture);
        var unit  = m.Groups[4].Value.Trim();
        if (unit == "" || unit == DegSign)
            return new AngleDeg(value);
        return ParseResult<AngleDeg>.NotOk($"Nierozpoznana jednostka '{unit}'");
    }
    

    public double Cos()
    {
        return MathEx.CosDeg((double)Degrees);
    }

    public string GetFriendlyDebugAngle()
    {
        var ma = (MinuteAngle)this;
        return $"{ToString(CultureInfo.CurrentCulture)} ({ma.RoundedHour}')";
    }

    public override int GetHashCode()
    {
        return Degrees.GetHashCode();
    }

    public bool IsZero()
    {
        return Degrees.Equals(decimal.Zero);
    }

    public double Sin()
    {
        return MathEx.SinDeg((double)Degrees);
    }

    public override string ToString()
    {
        return ToString(CultureInfo.CurrentCulture);
    }

    public string ToString(IFormatProvider formatProvider)
    {
        return Degrees.ToString(formatProvider) + DegSign;
    }

    public static AngleDeg Zero => new(0);

    public static AngleDeg Deg90 => new(90);

    public static AngleDeg Deg180 => new(180);

    public static AngleDeg Deg270 => new(270);

    public decimal Degrees { get; }

    public decimal Radians => Degrees * MathEx.DEGTORAD_m;

    public double RadiansAsDouble => (double)(Degrees * MathEx.DEGTORAD_m);

    public double DegreesAsDouble => (double)Degrees;

    public const string DegSign = "°";

    public static readonly Regex Re = new Regex($@"^{Length.FloatRegexp}(.*)$");
}
