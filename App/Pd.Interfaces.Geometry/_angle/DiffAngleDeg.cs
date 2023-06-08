using System;
using System.ComponentModel;
using System.Globalization;
using System.Text.RegularExpressions;
using iSukces.Mathematics;
using Newtonsoft.Json;
using Pd.Interfaces.Common;

namespace Pd.Interfaces.Geometry;

[ConvertFromStringCheckingMethod(nameof(IsValidDiffAngleDegString))]
[TypeConverter(typeof(DiffAngleDegTypeConverter))]
[JsonConverter(typeof(DiffAngleDegJsonConverter))]
[CompactSerializer]
public readonly partial struct DiffAngleDeg : IEquatable<DiffAngleDeg>
{
    [JsonConstructor]
    public DiffAngleDeg(decimal degrees)
    {
        Degrees = NormalizeDiffAngleDeg(degrees);
    }

    public DiffAngleDeg(int degrees)
    {
        Degrees = NormalizeDiffAngleDeg(degrees);
    }

    public DiffAngleDeg(double degrees)
    {
        Degrees = NormalizeDiffAngleDeg((decimal)degrees);
    }

    public static decimal AngleBetween(decimal a, decimal b)
    {
        var dif = Math.Abs(a - b);
        dif = Normalize0360(dif);
        if (dif > 180)
            dif = 360 - dif;
        return dif;
    }

    public static string IsValidDiffAngleDegString(object value, CultureInfo cultureInfo)
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


    private static decimal NormalizeDiffAngleDeg(decimal angle)
    {
        const int full = 360;
        if (angle > full)
            angle -= full;
        else if (angle < -full)
            angle += full;
        return angle;
    }


    public static DiffAngleDeg operator /(DiffAngleDeg angle, decimal number)
    {
        return new DiffAngleDeg(angle.Degrees / number);
    }

    public static bool operator ==(DiffAngleDeg left, DiffAngleDeg right)
    {
        return Equals(left, right);
    }

    public static explicit operator DiffAngleDeg(decimal x)
    {
        return new DiffAngleDeg(x);
    }

    public static explicit operator decimal(DiffAngleDeg x)
    {
        return x.Degrees;
    }

    public static implicit operator SinusCosinus(DiffAngleDeg x)
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

    public static bool operator !=(DiffAngleDeg left, DiffAngleDeg right)
    {
        return !Equals(left, right);
    }

    public static DiffAngleDeg operator *(DiffAngleDeg angle, decimal number)
    {
        return new DiffAngleDeg(angle.Degrees * number);
    }

    public static DiffAngleDeg operator *(decimal number, DiffAngleDeg angle)
    {
        return new DiffAngleDeg(angle.Degrees * number);
    }


    public static ParseResult<DiffAngleDeg> Parse(string x, NumberFormatInfo fi)
    {
        x = Length.ProcessNumber(x, fi);
        if (string.IsNullOrEmpty(x))
            return ParseResult<DiffAngleDeg>.NotOk(ValidationBase.NotEmpty);
        var m = Re.Match(x);
        if (!m.Success)
            return ParseResult<DiffAngleDeg>.NotOk($"wartość '{x}' nie jest prawidłowym oznaczenim kąta");
        var value = decimal.Parse(m.Groups[1].Value, CultureInfo.InvariantCulture);
        var unit  = m.Groups[4].Value.Trim();
        if (unit == "" || unit == DegSign)
            return new DiffAngleDeg(value);
        return ParseResult<DiffAngleDeg>.NotOk($"Nierozpoznana jednostka '{unit}'");
    }

    public double Cos()
    {
        return MathEx.CosDeg((double)Degrees);
    }

    public override bool Equals(object?  other)
    {
        if (other is null) return false;
        if (other.GetType() != typeof(DiffAngleDeg)) return false;
        return Equals((DiffAngleDeg)other);
    }

    public bool Equals(DiffAngleDeg other)
    {
        return Degrees.Equals(other.Degrees);
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

    public static DiffAngleDeg Zero => new(0);

    public static DiffAngleDeg Deg90 => new(90);

    public static DiffAngleDeg Deg180 => new(180);

    public static DiffAngleDeg Deg270 => new(270);

    public decimal Degrees { get; }

    public decimal Radians => Degrees * MathEx.DEGTORAD_m;

    public double RadiansAsDouble => (double)(Degrees * MathEx.DEGTORAD_m);

    public double DegreesAsDouble => (double)Degrees;

    public const string DegSign = "°";

    public static readonly Regex Re = new Regex($@"^{Length.FloatRegexp}(.*)$");
}

public sealed class DiffAngleDegTypeConverter : BasicTypeConverter<DiffAngleDeg>
{
    protected override DiffAngleDeg ConvertFromInternal(ITypeDescriptorContext context, CultureInfo culture,
        string value)
    {
        return DiffAngleDeg.Parse(value, culture.NumberFormat).GetValueOrThrow();
    }

    protected override string ConvertToInternal(ITypeDescriptorContext context, CultureInfo culture,
        DiffAngleDeg value, Type destinationType)
    {
        return value.ToString(culture);
    }
}

public sealed class DiffAngleDegJsonConverter : JsonConverter
{
    public override bool CanConvert(Type objectType)
    {
        return objectType == TypeBase;
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
        JsonSerializer serializer)
    {
        Console.WriteLine(reader.TokenType);
        switch (reader.TokenType)
        {
            case JsonToken.Integer:
                var longValue = (long)reader.Value;
                return (DiffAngleDeg)longValue;
            case JsonToken.Float:
                var decimalValue = (decimal)(double)reader.Value;
                return (DiffAngleDeg)decimalValue;
            case JsonToken.String:
            {
                var stringValue = ((string)reader.Value)?.Trim();
                if (string.IsNullOrEmpty(stringValue))
                    throw new JsonConverterDeserializeException(
                        "Unable to convert empty string into " + nameof(DiffAngleDeg), typeof(DiffAngleDeg));
                var result = DiffAngleDeg.Parse(stringValue, NumberFormatInfo.InvariantInfo);
                return result.GetValueOrThrow();
            }
        }

        throw new JsonConverterDeserializeException($"token \'{reader.TokenType}\'", TypeBase);
    }


    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        if (value == null)
        {
            writer.WriteNull();
            return;
        }

        var v = (DiffAngleDeg)value;
        writer.WriteValue(v.Degrees);
    }

    private static readonly Type TypeBase = typeof(DiffAngleDeg);
}
