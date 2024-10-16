using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Text.RegularExpressions;
using Alpex.Interfaces.Common;
using Newtonsoft.Json;

namespace Alpex.Interfaces.Geometry;

[TypeConverter(typeof(LengthTypeConverter))]
[JsonConverter(typeof(LengthJsonConverter))]
public struct Length : ICultureFormattable, IEquatable<Length>, IFormattable
{
    static Length()
    {
        Mnozniki = new Dictionary<string, decimal>
        {
            [LengthUnits.Meter]  = 1,
            [LengthUnits.Centimeter] = 0.01m,
            [LengthUnits.Milimeter] = 0.001m,
            [LengthUnits.Kilometer] = 1000
        };
    }

    public Length(decimal value, string unit)
        : this()
    {
        Unit  = unit;
        Value = value;
    }

    public static Length FromCentiMeter(decimal value)
    {
        return new Length(value, LengthUnits.Centimeter);
    }

    public static Length FromMeter(decimal value)
    {
        return new Length(value, LengthUnits.Meter);
    }

    public static Length FromMeter(int value)
    {
        return new Length(value, LengthUnits.Meter);
    }

    public static Length FromMeter(long value)
    {
        return new Length(value, LengthUnits.Meter);
    }

    public static Length FromMeter(double value)
    {
        return new Length((decimal)value, LengthUnits.Meter);
    }

    public static Length FromMm(decimal value)
    {
        return new Length(value, LengthUnits.Milimeter);
    }

    public static string? IsValidString(object value, CultureInfo cultureInfo)
    {
        return Parse(value as string, cultureInfo.NumberFormat).Error;
    }

    public static Length operator +(Length a, Length b)
    {
        return a.WithMeters(a.GetMeters() + b.GetMeters());
    }

    public static Length operator /(Length a, decimal b)
    {
        return new Length(a.Value / b, a.Unit);
    }

    public static bool operator ==(Length left, Length right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(Length left, Length right)
    {
        return !Equals(left, right);
    }

    public static Length operator -(Length a, Length b)
    {
        return a.WithMeters(a.GetMeters() - b.GetMeters());
    }

    public static Length operator -(Length l)
    {
        return new Length(-l.Value, l.Unit);
    }

    public static ParseResult<Length> Parse(string? x, NumberFormatInfo? fi)
    {
        x = ProcessNumber(x, fi);
        if (string.IsNullOrEmpty(x))
            return ParseResult<Length>.NotOk(ValidationBase.NotEmpty);
        var m = Re.Match(x);
        if (!m.Success)
        {
            return ParseResult<Length>.NotOk(string.Format(StrIsNotValid, x));
        }

        var     number = m.Groups[1].Value;
        decimal value;
        try
        {
            value = decimal.Parse(number, CultureInfo.InvariantCulture);
        }
        catch (OverflowException)
        {
            return ParseResult<Length>.NotOk(string.Format(StrNumberTooBigOrTooSmall, number));
        }

        var unit = m.Groups[4].Value.Trim();
        unit = ProcessUnit(unit);
        if (string.IsNullOrEmpty(unit))
            return FromMeter(value);
        if (Mnozniki.ContainsKey(unit))
            return new Length(value, unit);
        return ParseResult<Length>.NotOk(string.Format(StrUnknownUnit, unit));
    }

    public static string? ProcessNumber(string? x, NumberFormatInfo? fi)
    {
        fi ??= CultureInfo.CurrentCulture.NumberFormat;
        return x == null
            ? null
            : x.Trim()
                .Replace(fi.CurrencyGroupSeparator, "")
                .Replace(fi.NumberGroupSeparator, "")
                .Replace(fi.PercentGroupSeparator, "")
                .Replace(" ", "")
                .Replace(fi.CurrencyDecimalSeparator, ".")
                .Replace(fi.NumberDecimalSeparator, ".")
                .Replace(fi.PercentDecimalSeparator, ".");
    }

    public static string ProcessUnit(string unit)
    {
        unit = unit?.Trim() ?? LengthUnits.Meter;
        while (unit.Contains(" "))
            unit = unit.Replace(" ", "");
        while (unit.Contains("\t"))
            unit = unit.Replace("\t", "");
        return unit;
    }

    public override bool Equals(object?  other)
    {
        if (other is null) return false;
        if (other.GetType() != typeof(Length)) return false;
        return Equals((Length)other);
    }

    public bool Equals(Length other)
    {
        return Value.Equals(other.Value)
               && StringComparer.Ordinal.Equals(Unit, other.Unit);
    }

    public override int GetHashCode()
    {
        unchecked
        {
            return StringComparer.Ordinal.GetHashCode(Unit ?? string.Empty) * 397 ^ Value.GetHashCode();
        }
    }

    public decimal GetMeters()
    {
        if (Value == 0m || _unit == LengthUnits.Meter) return Value;
        if (Mnozniki.TryGetValue(_unit, out var mnoznik))
            return Value * mnoznik;
        throw new Exception($"Unknown unit '{_unit}'");
    }

    public decimal GetMm()
    {
        if (Value == 0m || _unit == LengthUnits.Milimeter) return Value;
        if (Mnozniki.TryGetValue(_unit, out var mnoznik))
            return Value * mnoznik * 1000;
        throw new Exception($"Unknown unit '{_unit}'");
    }

    public Length ToCm() => ToUnitIfPossible(LengthUnits.Centimeter);
    public Length ToMeter() => ToUnitIfPossible(LengthUnits.Meter);
    public Length ToMilimeter() => ToUnitIfPossible(LengthUnits.Milimeter);

    public Length RoundToMiliMeter()
    {
        int? decimals = null;
        switch (string.IsNullOrEmpty(_unit) ? LengthUnits.Meter : _unit)
        {
            case LengthUnits.Meter:
                decimals = 3;
                break;
            case LengthUnits.Centimeter:
                decimals = 1;
                break;
            case LengthUnits.Milimeter:
                decimals = 0;
                break;
            case LengthUnits.Kilometer:
                decimals = 6;
                break;
        }

        if (decimals == null)
            return this;
        return new Length(Math.Round(Value, decimals.Value), _unit);
    }

    public SimpleLength ToSimpleLength()
    {
        return new SimpleLength((double)GetMeters());
    }

    public override string ToString()
    {
        return ToString(null);
    }

    public string ToString(string? format, IFormatProvider? formatProvider)
    {
        var number = Value.ToString(format, formatProvider ?? CultureInfo.CurrentCulture);
        return string.IsNullOrEmpty(Unit) ? number : $"{number} {Unit}";
    }

    public string ToString(IFormatProvider? formatProvider)
    {
        var number = Value.ToString(formatProvider ?? CultureInfo.CurrentCulture);
        return string.IsNullOrEmpty(Unit) ? number : $"{number} {Unit}";
    }

    public Length ToUnitIfPossible(string unit)
    {
        if (string.IsNullOrEmpty(unit))
            unit = LengthUnits.Meter;
        if (unit == Unit)
            return this;
        if (Mnozniki.TryGetValue(unit, out var mnoznik))
            return new Length(GetMeters() / mnoznik, unit);

        return this;
    }

    public Length With(Length meters)
    {
        return WithMeters(meters.GetMeters());
    }

    public Length WithMeters(decimal meters)
    {
        if (string.IsNullOrEmpty(_unit))
            return FromMeter(meters);
        var mn = Mnozniki[_unit];
        return new Length(meters / mn, _unit);
    }

    public string Unit
    {
        get => _unit ?? "";
        private set
        {
            value = ProcessUnit(value);
            if (!Mnozniki.ContainsKey(value))
            {
                throw new Exception(string.Format(StrUnknownUnit, value));
            }

            _unit = value;
        }
    }


    public decimal Value { get; }

    public static string StrUnknownUnit            { get; set; } = "Nie rozpoznano jednostki \'{0}\'.";
    public static string StrNumberTooBigOrTooSmall { get; set; } = "Wartość \'{0}\' jest zbyt dużą lub zbyt małą liczbą";
    public static string StrIsNotValid             { get; set; } = "Wartość \'{0}\' nie jest poprawnym oznaczeniem odległości";

    public static string FloatRegexp = @"(\s*-?\s*?(\d+)(\.\d+)?)";

    private static readonly Regex Re = new Regex($@"^{FloatRegexp}(.*)$", RegexOptions.Compiled);

    public static readonly Dictionary<string, decimal> Mnozniki;

    private string? _unit;
}
