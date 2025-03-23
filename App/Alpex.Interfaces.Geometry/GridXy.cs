#nullable enable
using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using Alpex.Interfaces.Common;

namespace Alpex.Interfaces.Geometry;

[TypeConverter(typeof(GridXyTypeConverter))]
public struct GridXy : ICultureFormattable, IFormattable, IEquatable<GridXy>
{
    public GridXy(int x, int y)
    {
        X = Math.Max(0, x);
        Y = Math.Max(0, y);
    }

    public static bool operator ==(GridXy left, GridXy right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(GridXy left, GridXy right)
    {
        return !left.Equals(right);
    }

    public static ParseResult<GridXy> Parse(string? text, CultureInfo? culture)
    {
        var parts = Split(text);
        if (parts is null)
            return new GridXy();
        culture ??= CultureInfo.InvariantCulture;
        if (parts.Length != 2)
            return ParseResult<GridXy>.NotOk(ConversionTranslations.InvalidFormat);
        if (!int.TryParse(parts[0], NumberStyles.Any, culture, out var x))
            return ParseResult<GridXy>.NotOk(string.Format(ConversionTranslations.InvalidCountTextPar, "X"));
        if (!int.TryParse(parts[1], NumberStyles.Any, culture, out var y))
            return ParseResult<GridXy>.NotOk(string.Format(ConversionTranslations.InvalidCountTextPar, "Y"));
        return new GridXy(x, y);
    }

    public static string[]? Split(string? text)
    {
        text = text?.Trim();
        if (string.IsNullOrWhiteSpace(text))
            return null;
        return text.Split(Separators).Select(a => a.Trim()).ToArray();
    }

    public bool Equals(GridXy other)
    {
        return X == other.X && Y == other.Y;
    }

    public override bool Equals(object? obj)
    {
        return obj is GridXy other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y);
    }

    public bool IsEmpty()
    {
        return X < 1 || Y < 1;
    }

    public string ToString(IFormatProvider formatProvider)
    {
        var x = X.ToString(formatProvider);
        var y = Y.ToString(formatProvider);
        return $"{x} × {y}";
    }

    public string ToString(string? format, IFormatProvider? formatProvider)
    {
        var x = X.ToString(format, formatProvider);
        var y = Y.ToString(format, formatProvider);
        return $"{x} × {y}";
    }

    public int X { get; }
    public int Y { get; }

    public static char[] Separators = { 'x', 'X', '×', '*' };
}

public sealed class GridXyTypeConverter : BasicTypeConverter<GridXy>
{
    protected override GridXy ConvertFromInternal(ITypeDescriptorContext context, CultureInfo culture,
        string value)
    {
        var result = GridXy.Parse(value, CultureInfo.InvariantCulture);
        if (result.HasError)
            throw new FormatException(result.Error);
        return result.Value;
    }

    protected override string ConvertToInternal(ITypeDescriptorContext context, CultureInfo culture,
        GridXy value, Type destinationType)
    {
        return value.ToString(CultureInfo.InvariantCulture);
    }
}