using System;
using System.Globalization;
using Alpex.Interfaces.Common;
using Newtonsoft.Json;

namespace Alpex.Measure;

[JsonConverter(typeof(TeeConnectorEndPlusWireJsonConverter))]
[CompactSerializer(nameof(SerializeToCommaSeparatedString), nameof(DeserializeFromString))]
#if UML
    [UmlDiagram("Pipes", BackgroundColor = EditableTeeConnectorInternalWires.UmlBackgroundColor)]
    [UmlDiagram("TrojnikiAlarmowe", BackgroundColor = EditableTeeConnectorInternalWires.UmlBackgroundColor)]
    [UmlPackage("Węzeł z trójnikami")]
    [UmlDiagram(UmlDiagrams.CadAlarmWires)]
    [Auto.EqualityGeneratorAttribute]
    [Auto.ComparerGenerator(nameof(End), nameof(WireIdx))]
#endif
public readonly struct TeeConnectorEndPlusWire
    : IEquatable<TeeConnectorEndPlusWire>, IComparable<TeeConnectorEndPlusWire>, IComparable
{
    public TeeConnectorEndPlusWire(TeeConnectorEnds end, XWireRouterInputWire wireIdx)
    {
        End     = end;
        WireIdx = wireIdx;
    }

    public static TeeConnectorEndPlusWire DeserializeFromString(string end, string wireIdx)
    {
        end = end.Trim();
        TeeConnectorEnds xEnd;
        if (string.IsNullOrEmpty(end))
            xEnd = default;
        else
        {
            if (int.TryParse(end, NumberStyles.Any, CultureInfo.InvariantCulture, out var number))
                xEnd = (TeeConnectorEnds)number;
            else
                xEnd = (TeeConnectorEnds)Enum.Parse(typeof(TeeConnectorEnds), end, true);
        }

        wireIdx = wireIdx.Trim();
        var xWireIdx = string.IsNullOrEmpty(wireIdx)
            ? default
            : wireIdx.ParseIntInvariant().AsWireRouterInputWire();

        var result = new TeeConnectorEndPlusWire(xEnd, xWireIdx);
        return result;
    }

    public static TeeConnectorEndPlusWire DeserializeFromString(string end)
    {
        var q = end.Split(',');
        return DeserializeFromString(q[0], q[1]);
    }

    public string SerializeToCommaSeparatedString()
    {
        return string.Format("{0},{1}", ((int)End).ToInvariantString(), WireIdx.Value.ToInvariantString());
    }

    public override string ToString()
    {
        return $"{End}({WireIdx.ValueAsText()})";
    }

    public TeeConnectorEnds     End     { get; }
    public XWireRouterInputWire WireIdx { get; }

    public int CompareTo(TeeConnectorEndPlusWire other)
    {
        var comparisonResult = End.CompareTo(other.End);
        if (comparisonResult != 0) return comparisonResult;
        return WireIdx.CompareTo(other.WireIdx);
    }

    public int CompareTo(object? obj)
    {
        if (obj is null) return 1;
        return obj is TeeConnectorEndPlusWire other
            ? CompareTo(other)
            : throw new ArgumentException("Object must be of type TeeConnectorEndPlusWire");
    }

    public override bool Equals(object? other)
    {
        if (other is null) return false;
        if (other.GetType() != typeof(TeeConnectorEndPlusWire)) return false;
        return Equals((TeeConnectorEndPlusWire)other);
    }

    public bool Equals(TeeConnectorEndPlusWire other)
    {
        return End == other.End
               && WireIdx.Equals(other.WireIdx);
    }

    public override int GetHashCode()
    {
        unchecked
        {
            return WireIdx.GetHashCode() * 397 ^ End.GetHashCode();
        }
    }

    public static bool operator !=(TeeConnectorEndPlusWire left, TeeConnectorEndPlusWire right)
    {
        return !Equals(left, right);
    }

    public static bool operator <(TeeConnectorEndPlusWire left, TeeConnectorEndPlusWire right)
    {
        if (Equals(left, right)) return false;
        return left.CompareTo(right) < 0;
    }

    public static bool operator <=(TeeConnectorEndPlusWire left, TeeConnectorEndPlusWire right)
    {
        if (Equals(left, right)) return true;
        return left.CompareTo(right) <= 0;
    }

    public static bool operator ==(TeeConnectorEndPlusWire left, TeeConnectorEndPlusWire right)
    {
        return Equals(left, right);
    }

    public static bool operator >(TeeConnectorEndPlusWire left, TeeConnectorEndPlusWire right)
    {
        if (Equals(left, right)) return false;
        return left.CompareTo(right) > 0;
    }

    public static bool operator >=(TeeConnectorEndPlusWire left, TeeConnectorEndPlusWire right)
    {
        if (Equals(left, right)) return true;
        return left.CompareTo(right) >= 0;
    }
}
