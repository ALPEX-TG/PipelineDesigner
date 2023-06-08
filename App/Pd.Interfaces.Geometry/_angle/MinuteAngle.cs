using System;
using System.ComponentModel;
using System.Globalization;
using System.Text.RegularExpressions;
using iSukces.Mathematics;
using Newtonsoft.Json;
using Pd.Interfaces.Common;

namespace Pd.Interfaces.Geometry;

[ConvertFromStringCheckingMethod(nameof(IsValidMinuteAngleString))]
[TypeConverter(typeof(MinuteAngleTypeConverter))]
[JsonConverter(typeof(MinuteAngleJsonConverter))]
[CompactSerializer]
public readonly partial struct MinuteAngle : IEquatable<MinuteAngle>
{
    [JsonConstructor]
    public MinuteAngle(int value)
    {
        Minutes = NormalizeMinuteAngle(value);
    }

    public static decimal AngleBetween(decimal a, decimal b)
    {
        var dif = Math.Abs(a - b);
        dif = Normalize0360(dif);
        if (dif > 180)
            dif = 360 - dif;
        return dif;
    }

    public static MinuteAngle FromHour(int hour)
    {
        return new MinuteAngle(5 * (hour % 12));
    }

    public static string IsValidMinuteAngleString(object value, CultureInfo cultureInfo)
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


    public static int NormalizeMinuteAngle(int minutes)
    {
        if (minutes < 0)
        {
            var tmp = minutes / 60 - 1;
            minutes -= tmp * 60;
            return minutes;
        }

        return minutes >= 60 ? minutes - minutes / 60 * 60 : minutes;
    }

    public static bool operator ==(MinuteAngle left, MinuteAngle right)
    {
        return Equals(left, right);
    }


    public static explicit operator MinuteAngle(int x)
    {
        return new MinuteAngle(x);
    }

    public static explicit operator decimal(MinuteAngle x)
    {
        return x.Minutes;
    }

    public static implicit operator SinusCosinus(MinuteAngle x)
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

    public static bool operator !=(MinuteAngle left, MinuteAngle right)
    {
        return !Equals(left, right);
    }

    public static MinuteAngle operator *(MinuteAngle angle, int number)
    {
        return new MinuteAngle(angle.Minutes * number);
    }

    public static MinuteAngle operator *(int number, MinuteAngle angle)
    {
        return new MinuteAngle(angle.Minutes * number);
    }


    public static ParseResult<MinuteAngle> Parse(string x, NumberFormatInfo fi)
    {
        x = Length.ProcessNumber(x, fi);
        if (string.IsNullOrEmpty(x))
            return ParseResult<MinuteAngle>.NotOk(ValidationBase.NotEmpty);
        var m = Re.Match(x);
        if (!m.Success)
            return ParseResult<MinuteAngle>.NotOk($"wartość '{x}' nie jest prawidłowym oznaczenim kąta w minutach");
        var value = int.Parse(m.Groups[1].Value, CultureInfo.InvariantCulture);
        var unit  = m.Groups[4].Value.Trim();
        if (unit == "" || unit == MinutesSign)
            return new MinuteAngle(value);
        return ParseResult<MinuteAngle>.NotOk($"Nierozpoznana jednostka '{unit}'");
    }

    public override bool Equals(object?  other)
    {
        if (other is null) return false;
        if (other.GetType() != typeof(MinuteAngle)) return false;
        return Equals((MinuteAngle)other);
    }

    public bool Equals(MinuteAngle other)
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

    public static MinuteAngle Zero => new(0);

    public int Minutes { get; }

    [JsonIgnore]
    public int RoundedHour => (0.2 * Minutes).Round();

    [JsonIgnore]
    public int RoundedHour2 => (Minutes + 3) / 5;

    public const string MinutesSign = "\"";


    public static readonly Regex Re = new Regex($@"^{Length.FloatRegexp}(.*)$");
}

public sealed class MinuteAngleTypeConverter : BasicTypeConverter<MinuteAngle>
{
    protected override MinuteAngle ConvertFromInternal(ITypeDescriptorContext context, CultureInfo culture,
        string value)
    {
        return MinuteAngle.Parse(value, culture.NumberFormat).GetValueOrThrow();
    }

    protected override string ConvertToInternal(ITypeDescriptorContext context, CultureInfo culture,
        MinuteAngle value, Type destinationType)
    {
        return value.ToString(culture);
    }
}

public sealed class MinuteAngleJsonConverter : JsonConverter
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
                return (MinuteAngle)longValue;
            case JsonToken.Float:
                var decimalValue = (decimal)(double)reader.Value;
                return (MinuteAngle)decimalValue;
            case JsonToken.String:
            {
                var stringValue = ((string)reader.Value)?.Trim();
                if (string.IsNullOrEmpty(stringValue))
                    throw new JsonConverterDeserializeException(
                        "Unable to convert empty string into " + nameof(MinuteAngle), typeof(MinuteAngle));
                var result = MinuteAngle.Parse(stringValue, NumberFormatInfo.InvariantInfo);
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

        var v = (MinuteAngle)value;
        writer.WriteValue(v.Minutes);
    }

    private static readonly Type TypeBase = typeof(MinuteAngle);
}
