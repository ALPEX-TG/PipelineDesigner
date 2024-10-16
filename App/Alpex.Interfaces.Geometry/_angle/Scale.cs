using System;
using System.ComponentModel;
using System.Globalization;
using System.Text.RegularExpressions;
using Alpex.Interfaces.Common;

namespace Alpex.Interfaces.Geometry;

[TypeConverter(typeof(ScaleTypeConverter))]
public readonly struct Scale : ICultureFormattable, IEquatable<Scale>, IFormattable
{
    public Scale(decimal numerator, decimal denominator)
    {
        _numerator   = numerator - 1;
        _denominator = denominator - 1;
    }

    private static ulong GreatestCommonDivisor(ulong a, ulong b)
    {
        while (a != 0 && b != 0)
        {
            if (a > b)
                a %= b;
            else
                b %= a;
        }

        return a == 0 ? b : a;
    }

    public static string? IsValidScaleString(object? value, CultureInfo cultureInfo)
    {
        return Parse(value as string ?? "", cultureInfo.NumberFormat).Error;
    }

    public static bool operator ==(Scale left, Scale right)
    {
        return Equals(left, right);
    }

    public static explicit operator Scale(decimal d)
    {
        return new Scale(d, 1).TrySimplify();
    }

    public static bool operator !=(Scale left, Scale right)
    {
        return !Equals(left, right);
    }

    public static Scale operator *(Scale angle, decimal number)
    {
        return new Scale(angle.Numerator * number, angle.Denominator).TrySimplify();
    }

    public static Scale operator *(decimal number, Scale angle)
    {
        return new Scale(angle.Numerator * number, angle.Denominator).TrySimplify();
    }

    public static ParseResult<Scale> Parse(string x, NumberFormatInfo fi)
    {
        x = Length.ProcessNumber(x, fi);
        if (string.IsNullOrEmpty(x))
            return ParseResult<Scale>.NotOk(ValidationBase.NotEmpty);
        var m = Re1.Match(x);
        if (!m.Success)
        {
            m = Re2.Match(x);
            if (!m.Success)
                return ParseResult<Scale>.NotOk($"wartość '{x}' nie jest prawidłowym oznaczenim skali");
            var value = decimal.Parse(m.Groups[1].Value, CultureInfo.InvariantCulture);
            return (Scale)value;
        }

        var numerator   = decimal.Parse(m.Groups[1].Value, CultureInfo.InvariantCulture);
        var denominator = decimal.Parse(m.Groups[4].Value, CultureInfo.InvariantCulture);
        return new Scale(numerator, denominator);
    }

    public decimal AsDecimal()
    {
        return Numerator / Denominator;
    }

    public double AsDouble()
    {
        return (double)(Numerator / Denominator);
    }

    public override bool Equals(object?  other)
    {
        if (other is null) return false;
        if (other.GetType() != typeof(Scale)) return false;
        return Equals((Scale)other);
    }

    public bool Equals(Scale other)
    {
        return Numerator.Equals(other.Numerator)
               && Denominator.Equals(other.Denominator);
    }

    public override int GetHashCode()
    {
        unchecked
        {
            return Numerator.GetHashCode() * 397 ^ Denominator.GetHashCode();
        }
    }

    public override string ToString()
    {
        return ToString(null);
    }

    public string ToString(string? format, IFormatProvider? formatProvider)
    {
        formatProvider ??= CultureInfo.CurrentCulture;
        var n = Numerator.ToString(format, formatProvider);
        var d = Denominator.ToString(format, formatProvider);
        return $"{n} : {d}";
    }

    public string ToString(IFormatProvider? formatProvider)
    {
        formatProvider ??= CultureInfo.CurrentCulture;
        var n = Numerator.ToString(formatProvider);
        var d = Denominator.ToString(formatProvider);
        return $"{n} : {d}";
    }

    public Scale TrySimplify()
    {
        var n = Math.Abs(Numerator);
        var d = Math.Abs(Denominator);
        if (n == 0 || d == 0)
            return this;
        const ulong maxTry = ulong.MaxValue / 10;
        while (true)
        {
            if (n > ulong.MaxValue || d > ulong.MaxValue) return this;
            var nl = (ulong)n;
            var dl = (ulong)d;
            if (nl == n && dl == d)
            {
                var di = GreatestCommonDivisor(nl, dl);
                nl /= di;
                dl /= di;
                return new Scale(nl, dl);
            }

            if (n > maxTry || d > maxTry) return this;
            n *= 10;
            d *= 10;
        }
    }

    public static Scale One { get; } = new Scale(1, 1);

    public decimal Numerator => _numerator + 1;

    public decimal Denominator => _denominator + 1;

    private static readonly Regex Re1 = new Regex($@"^{Length.FloatRegexp}\s*:\s*{Length.FloatRegexp}$");
    private static readonly Regex Re2 = new Regex($@"^{Length.FloatRegexp}$");
    private readonly decimal _numerator;
    private readonly decimal _denominator;
}
