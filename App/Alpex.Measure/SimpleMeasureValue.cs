using System;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Alpex.Interfaces.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Alpex.Measure;

[ImmutableObject(true)]
[JsonConverter(typeof(SimpleMeasureValueJsonConverter))]
public sealed class SimpleMeasureValue : IMeasuredValue, IEquatable<SimpleMeasureValue>
{
    static SimpleMeasureValue()
    {
        Empty         = new SimpleMeasureValue();
        NotMeasurable = new SimpleMeasureValue(MeasureStatus.NotMeasurable, decimal.Zero, MeasureUnit.Empty);
    }

    [JsonConstructor]
    public SimpleMeasureValue(MeasureStatus status, decimal value, MeasureUnit? unit)
    {
        if (status is MeasureStatus.Empty or MeasureStatus.NotMeasurable)
        {
            value = 0;
            unit  = null;
        }

        Value  = value;
        Unit   = unit;
        Status = status;
    }

    private SimpleMeasureValue()
    {
        Status = MeasureStatus.Empty;
    }

    public static bool operator ==(SimpleMeasureValue left, SimpleMeasureValue right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(SimpleMeasureValue left, SimpleMeasureValue right)
    {
        return !Equals(left, right);
    }


    public static ParseResult<SimpleMeasureValue> Parse(string? text)
    {
        text = text?.Trim();
        if (text == "?")
            return new SimpleMeasureValue(MeasureStatus.NotMeasurable, 0, MeasureUnit.Empty);
        if (string.IsNullOrEmpty(text))
            return Empty;
        var m = ParseRegex.Match(text);
        if (!m.Success)
            return Err("");

        MeasureUnit?  unit;
        decimal       value;
        MeasureStatus oper;
        {
            var unitText = m.Groups[3].Value.Trim();
            if (string.IsNullOrEmpty(unitText))
                unit = MeasureUnit.Empty;
            else if (!MeasureUnit.WellKnown.All.TryGetValue(unitText, out unit))
                return Err("Nieznana jednostka miary.");
        }
        {
            var valueText = m.Groups[2].Value.Trim();
            if (!decimal.TryParse(valueText, NumberStyles.Any, CultureInfo.InvariantCulture, out value))
                return Err("Błędna liczba.");
        }
        {
            var prefix = m.Groups[1].Value.Trim();
            if (!CompareOperatorExt.TryParse(prefix, out oper))
                return Err("Błędny prefix pomiaru.");
        }

        return new SimpleMeasureValue(oper, value, unit);

        ParseResult<SimpleMeasureValue> Err(string msg)
        {
            var error = $"wartość '{text}' nie jest prawidłowym oznaczenim wielkości mierzonej.";
            if (!string.IsNullOrEmpty(msg))
                error += " " + msg;
            return ParseResult<SimpleMeasureValue>.NotOk(error);
        }
    }

    public override bool Equals(object? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return other is SimpleMeasureValue otherCasted && Equals(otherCasted);
    }

    public bool Equals(SimpleMeasureValue? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return Status == other.Status
               && Value.Equals(other.Value)
               && Equals(Unit, other.Unit);
    }

    public override int GetHashCode()
    {
        unchecked
        {
            return (Value.GetHashCode() * 397 ^ (Unit?.GetHashCode() ?? 0)) * 8 + (int)Status;
        }
    }


    public bool IsEmpty()
    {
        return Status == MeasureStatus.Empty;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private bool IsMeasurable()
    {
        return Status != MeasureStatus.NotMeasurable;
    }

    public string Serialize()
    {
        return Serialize(CultureInfo.InvariantCulture, false);
    }

    public string Serialize(CultureInfo ci, bool spaces)
    {
        if (IsEmpty())
            return "";
        if (!IsMeasurable())
            return MeasureStatus.NotMeasurable.FriendlyPrefix();
        var sp     = spaces ? " " : "";
        var result = Status.FriendlyPrefix() + sp + Value.ToString(ci);
        if (Unit is null || Unit.IsEmpty)
            return result;
        var unit = Unit.UnitName;

        if (MeasureUnit.WellKnown.All.ContainsKey(unit))
            return result + sp + unit;
        throw new Exception("Unknown unit");
    }

    public bool ShouldSerializeStatus()
    {
        return Status != MeasureStatus.None;
    }

    public bool ShouldSerializeUnit()
    {
        return IsMeasurable() && Unit is not null && !Unit.IsEmpty;
    }

    public bool ShouldSerializeValue()
    {
        return Value != 0m && IsMeasurable();
    }

    public override string ToString()
    {
        return Status switch
        {
            MeasureStatus.Empty => "",
            MeasureStatus.NotMeasurable => "?",
            _ => Status.FriendlyPrefix() + Value + Unit?.UnitName
        };
    }

    public static SimpleMeasureValue Empty         { get; }
    public static SimpleMeasureValue NotMeasurable { get; }


    public decimal Value { get; }


    public MeasureUnit? Unit { get; }

    [JsonConverter(typeof(StringEnumConverter))]
    public MeasureStatus Status { get; }

    private const string ParseFilter = @"([^\d]*)\s*(\d+(?:\.\d*)?)\s*([^\s\d][^\d]*)";


    private static readonly Regex ParseRegex =
        new Regex(ParseFilter, RegexOptions.IgnoreCase | RegexOptions.Compiled);
}
