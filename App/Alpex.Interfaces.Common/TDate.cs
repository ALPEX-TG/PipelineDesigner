using System;
using Newtonsoft.Json;

namespace Alpex.Interfaces.Common;

[CompactSerializer]
[JsonConverter(typeof(TDateJsonConverter))]
public readonly struct TDate : IEquatable<TDate>, IComparable<TDate>, IComparable
{
    public TDate(DateTime value)
    {
        Value = value.Date;
    }

    public static bool operator ==(TDate left, TDate right)
    {
        return left.Equals(right);
    }

    public static bool operator >(TDate left, TDate right)
    {
        return left.CompareTo(right) > 0;
    }

    public static bool operator >=(TDate left, TDate right)
    {
        return left.CompareTo(right) >= 0;
    }

    public static bool operator !=(TDate left, TDate right)
    {
        return !left.Equals(right);
    }

    public static bool operator <(TDate left, TDate right)
    {
        return left.CompareTo(right) < 0;
    }

    public static bool operator <=(TDate left, TDate right)
    {
        return left.CompareTo(right) <= 0;
    }

    public static TDate Parse(string? text)
    {
        if (TryParse(text ?? "", out var value))
            return value;
        var message = string.Format(Translations1.StrUnableToConvertParIntoDate.Value, text);
        throw new ArgumentException(message);
    }

    public static bool TryParse(string? text, out TDate value)
    {
        text = text?.Trim() ?? "";
        if (text.Length == 10)
        {
            var y = int.Parse(text.Substring(0, 4));
            var m = int.Parse(text.Substring(5, 2));
            var d = int.Parse(text.Substring(8, 2));
            value = new TDate(new DateTime(y, m, d));
            return true;
        }

        value = default;
        return false;
    }

    public int CompareTo(TDate other)
    {
        return Value.CompareTo(other.Value);
    }

    public int CompareTo(object? obj)
    {
        if (ReferenceEquals(null, obj)) return 1;
        return obj is TDate other
            ? CompareTo(other)
            : throw new ArgumentException($"Object must be of type {nameof(TDate)}");
    }

    public bool Equals(TDate other)
    {
        return Value.Equals(other.Value);
    }

    public override bool Equals(object? obj)
    {
        return obj is TDate other && Equals(other);
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }

    public string ToReportString()
    {
        return Value.ToString("yyyy-MM-dd");
    }

    public DateTime Value { get; }
}

public sealed class TDateJsonConverter : JsonConverter
{
    public override bool CanConvert(Type objectType)
    {
        return objectType == typeof(TDate);
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
        JsonSerializer serializer)
    {
        switch (reader.Value)
        {
            case string i:
                return TDate.Parse(i);
            case null when objectType == typeof(TDate?):
                return null;
        }

        throw new NotImplementedException();
    }

    public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
    {
        var v = (TDate)value;
        writer.WriteValue(v.Value.ToString("yyyy-MM-dd"));
    }
}
