using System;
using System.ComponentModel;
using System.Globalization;
using Alpex.Interfaces.Common;

namespace Alpex.Interfaces.Geometry;

[TypeConverter(typeof(Length3DTypeConverter))]
public struct Length3D : ICultureFormattable, IFormattable
{
    public Length3D(Length x, Length y, Length z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public static Length3D FromMm(decimal x, decimal y, decimal z)
    {
        return new Length3D(Length.FromMm(x), Length.FromMm(y), Length.FromMm(z));
    }

    public static Length3D FromMeter(decimal x, decimal y, decimal z)
    {
        return new Length3D(Length.FromMeter(x), Length.FromMeter(y), Length.FromMeter(z));
    }

    public static ParseResult<Length3D> Parse(string? text, CultureInfo? culture)
    {
        var parts = GridXy.Split(text);
        if (parts is null)
            return new Length3D();
        culture = culture ?? CultureInfo.InvariantCulture;


        if (parts.Length != 3)
            return ParseResult<Length3D>.NotOk(ConversionTranslations.InvalidFormat);

        var x = Length.Parse(parts[0], culture.NumberFormat);
        if (x.HasError)
            return ParseResult<Length3D>.NotOk(string.Format(ConversionTranslations.InvalidDimensionTextParPar, "X",
                x.Error));

        var y = Length.Parse(parts[1], culture.NumberFormat);
        if (y.HasError)
            return ParseResult<Length3D>.NotOk(string.Format(ConversionTranslations.InvalidDimensionTextParPar, "Y",
                y.Error));

        var z = Length.Parse(parts[2], culture.NumberFormat);
        if (z.HasError)
            return ParseResult<Length3D>.NotOk(string.Format(ConversionTranslations.InvalidDimensionTextParPar, "Z",
                z.Error));

        return new Length3D(x.Value, y.Value, z.Value);
    }

    public Length3D ToCentiMeter()
    {
        return ToUnitIfPossible(LengthUnits.Centimeter);
    }

    public Length3D ToMilimeter()
    {
        return ToUnitIfPossible(LengthUnits.Milimeter);
    }
    public Length3D ToMeter()
    {
        return ToUnitIfPossible(LengthUnits.Meter);
    }

    public string ToString(IFormatProvider? formatProvider)
    {
        formatProvider ??= CultureInfo.CurrentCulture;
        var ax = X.ToString(formatProvider);
        var ay = Y.ToString(formatProvider);
        var az = Z.ToString(formatProvider);
        return $"{ax} × {ay} × {az}";
    }

    public override string ToString()
    {
        return ToString(null);
    }

    public string ToString(string? format, IFormatProvider? formatProvider)
    {
        formatProvider ??= CultureInfo.CurrentCulture;
        var ax = X.ToString(format, formatProvider);
        var ay = Y.ToString(format, formatProvider);
        var az = Z.ToString(format, formatProvider);
        return $"{ax} × {ay} × {az}";
    }

    public Length3D ToUnitIfPossible(string unit)
    {
        return new Length3D(
            X.ToUnitIfPossible(unit), Y.ToUnitIfPossible(unit), Z.ToUnitIfPossible(unit));
    }

    public Length X { get; }
    public Length Y { get; }
    public Length Z { get; }

    public bool HasVolume()
    {
        return X.Value > 0 && Y.Value > 0 && Z.Value > 0;
    }
}

public sealed class Length3DTypeConverter : BasicTypeConverter<Length3D>
{
    protected override Length3D ConvertFromInternal(ITypeDescriptorContext context, CultureInfo culture,
        string value)
    {
        var result = Length3D.Parse(value, CultureInfo.InvariantCulture);
        if (result.HasError)
            throw new FormatException(result.Error);
        return result.Value;
    }

    protected override string ConvertToInternal(ITypeDescriptorContext context, CultureInfo culture,
        Length3D value, Type destinationType)
    {
        return value.ToString(CultureInfo.InvariantCulture);
    }
}