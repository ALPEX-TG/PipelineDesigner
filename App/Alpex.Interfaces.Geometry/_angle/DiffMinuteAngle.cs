using System;
using System.ComponentModel;
using System.Globalization;
using System.Text.RegularExpressions;
using Alpex.Interfaces.Common;
using iSukces.Mathematics;
using Newtonsoft.Json;

namespace Alpex.Interfaces.Geometry;

[ConvertFromStringCheckingMethod(nameof(IsValidDiffMinuteAngleString))]
[TypeConverter(typeof(DiffMinuteAngleTypeConverter))]
[JsonConverter(typeof(DiffMinuteAngleJsonConverter))]
[CompactSerializer]
public readonly partial struct DiffMinuteAngle : IEquatable<DiffMinuteAngle>
{
    [JsonConstructor]
    public DiffMinuteAngle(int value)
    {
        Minutes = NormalizeDiffMinuteAngle(value);
    }


    public static decimal AngleBetween(decimal a, decimal b)
    {
        var dif = Math.Abs(a - b);
        dif = Normalize0360(dif);
        if (dif > 180)
            dif = 360 - dif;
        return dif;
    }

    public static string IsValidDiffMinuteAngleString(object value, CultureInfo cultureInfo)
    {
        return Parse(value as string, cultureInfo.NumberFormat).Error;
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

    public static int NormalizeDiffMinuteAngle(int minutes)
    {
        const int full = 60;
        if (minutes > full)
            minutes -= full;
        else if (minutes < -full)
            minutes += full;
        return minutes;

        /*
        if (minutes <= -60)
        {
            var tmp =minutes / 60 -1;
            minutes -= tmp * 60;
            return minutes;
        }
        return minutes >= 60 ? minutes -  minutes / 60 * 60 : minutes;
    */
    }

    public static bool operator ==(DiffMinuteAngle left, DiffMinuteAngle right)
    {
        return Equals(left, right);
    }


    public static explicit operator DiffMinuteAngle(int x)
    {
        return new DiffMinuteAngle(x);
    }

    public static explicit operator decimal(DiffMinuteAngle x)
    {
        return x.Minutes;
    }

    public static implicit operator SinusCosinus(DiffMinuteAngle x)
    {
        if (x.Minutes == 0m)
            return new SinusCosinus(0, 1);
        if (x.Minutes == 180m)
            return new SinusCosinus(0, -1);
        if (x.Minutes == 90m)
            return new SinusCosinus(1, 0);
        if (x.Minutes == 270m)
            return new SinusCosinus(-1, 0);
        return SinusCosinus.FromAngleDeg(x.Minutes);
    }

    public static bool operator !=(DiffMinuteAngle left, DiffMinuteAngle right)
    {
        return !Equals(left, right);
    }

    public static DiffMinuteAngle operator *(DiffMinuteAngle angle, int number)
    {
        return new DiffMinuteAngle(angle.Minutes * number);
    }

    public static DiffMinuteAngle operator *(int number, DiffMinuteAngle angle)
    {
        return new DiffMinuteAngle(angle.Minutes * number);
    }

    public static DiffMinuteAngle operator -(DiffMinuteAngle left, DiffMinuteAngle right)
    {
        return new DiffMinuteAngle(left.Minutes - right.Minutes);
    }


    public static ParseResult<DiffMinuteAngle> Parse(string x, NumberFormatInfo fi)
    {
        x = Length.ProcessNumber(x, fi);
        if (string.IsNullOrEmpty(x))
            return ParseResult<DiffMinuteAngle>.NotOk(ValidationBase.NotEmpty);
        var m = Re.Match(x);
        if (!m.Success)
            return ParseResult<DiffMinuteAngle>.NotOk($"wartość '{x}' nie jest prawidłowym oznaczenim kąta w minutach");
        var value = int.Parse(m.Groups[1].Value, CultureInfo.InvariantCulture);
        var unit  = m.Groups[4].Value.Trim();
        if (unit == "" || unit == MinutesSign)
            return new DiffMinuteAngle(value);
        return ParseResult<DiffMinuteAngle>.NotOk($"Nierozpoznana jednostka '{unit}'");
    }

    public override bool Equals(object?  other)
    {
        if (other is null) return false;
        if (other.GetType() != typeof(DiffMinuteAngle)) return false;
        return Equals((DiffMinuteAngle)other);
    }

    public bool Equals(DiffMinuteAngle other)
    {
        return Minutes == other.Minutes;
    }

    public override int GetHashCode()
    {
        return Minutes;
    }


    public bool IsZero()
    {
        return Minutes == 0;
    }


    public override string ToString()
    {
        return ToString(CultureInfo.CurrentCulture);
    }

    public string ToString(IFormatProvider formatProvider)
    {
        return Minutes.ToString(formatProvider) + MinutesSign;
    }

    public static DiffMinuteAngle Zero => new(0);

    public int Minutes { get; }

    public const string MinutesSign = "\"";


    public static readonly Regex Re = new Regex($@"^{Length.FloatRegexp}(.*)$");
}
