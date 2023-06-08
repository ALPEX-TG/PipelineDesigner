using System;
using System.Globalization;
using System.Text;
using Newtonsoft.Json;
using Pd.Interfaces.Common;

namespace Pd.Measure;

/// <summary>
///     Przechowuje parę identyfikatorów jednoznacznie określających koniec pętli
/// </summary>
#if UML
    [Auto.EqualityGeneratorAttribute]
    [Auto.ShouldSerializeInfoAttribute("{0}.HasAnyValue()")]
    [UmlDataStructure]
#endif
[CompactSerializer(nameof(SerializeCompact), nameof(DeserializeCompact))]
[JsonConverter(typeof(VtCompactLoopEndReferenceJsonConverter))]
public readonly struct VtCompactLoopEndReference
    : IEquatable<VtCompactLoopEndReference>
{
    // feature 52DD1E70-C231-4ADF-82D8-F6CC97216B0B

    public VtCompactLoopEndReference(XAlarmLoopUid loopId, VtPad padId)
    {
        LoopId = loopId;
        PadId  = padId;
    }


    public static VtCompactLoopEndReference DeserializeCompact(string serialized)
    {
        if (string.IsNullOrEmpty(serialized))
            return Empty;
        var comma        = serialized.Split('|');
        var loopUid      = XAlarmLoopUid.Parse(comma[0]);
        var componentUid = XAlarmContainerUid.Parse(comma[1]);
        var endIndex     = int.Parse(comma[2], NumberStyles.Any, CultureInfo.InvariantCulture);
        var wireIndex    = int.Parse(comma[3], NumberStyles.Any, CultureInfo.InvariantCulture).AsWireRouterInputWire();

        return new VtCompactLoopEndReference(loopUid, new VtPad(componentUid, endIndex, wireIndex));
    }

    public bool HasAnyValue()
    {
        return !LoopId.IsEmpty || !PadId.IsEmpty();
    }

    public override string ToString()
    {
        if (IsEmpty)
            return "EMPTY";
        return $"{PadId} at {LoopId}";
    }

    public string SerializeCompact()
    {
        if (IsEmpty)
            return "";
        var sb = new StringBuilder(64);
        sb.Append(LoopId.Value.ToString("N"));
        sb.Append('|');

        sb.Append(PadId.ComponentUid.Value.ToString("N"));
        sb.Append('|');

        sb.Append(PadId.EndIndex.ToInvariantString());
        sb.Append('|');

        sb.Append(PadId.WireIndex.Value.ToInvariantString());
        return sb.ToString();
    }

    public static VtCompactLoopEndReference Empty { get; } = new(XAlarmLoopUid.Empty, VtPad.Empty);

    public XAlarmLoopUid LoopId { get; }

    public VtPad PadId { get; }

    [JsonIgnore]
    public bool IsEmpty => LoopId.IsEmpty || PadId.IsEmpty();

    private sealed class VtCompactLoopEndReferenceJsonConverter : JsonConverter<VtCompactLoopEndReference>
    {
        public override VtCompactLoopEndReference ReadJson(JsonReader reader, Type objectType,
            VtCompactLoopEndReference existingValue,
            bool hasExistingValue, JsonSerializer serializer)
        {
            switch (reader.Value)
            {
                case null:
                    return Empty;
                case string s:
                    return DeserializeCompact(s);
                default:
                    throw new NotImplementedException();
            }
        }

        public override void WriteJson(JsonWriter writer, VtCompactLoopEndReference value, JsonSerializer serializer)
        {
            var s = value.SerializeCompact();
            writer.WriteValue(s);
        }
    }

    public override bool Equals(object? other)
    {
        if (other is null) return false;
        if (other.GetType() != typeof(VtCompactLoopEndReference)) return false;
        return Equals((VtCompactLoopEndReference)other);
    }

    public bool Equals(VtCompactLoopEndReference other)
    {
        return LoopId.Equals(other.LoopId)
               && PadId.Equals(other.PadId);
    }

    public override int GetHashCode()
    {
        unchecked
        {
            return LoopId.GetHashCode() * 397 ^ PadId.GetHashCode();
        }
    }

    public bool ShouldSerializeLoopId()
    {
        return !LoopId.IsEmpty;
    }

    public bool ShouldSerializePadId()
    {
        return !PadId.IsEmpty();
    }

    public static bool operator !=(VtCompactLoopEndReference left, VtCompactLoopEndReference right)
    {
        return !Equals(left, right);
    }

    public static bool operator ==(VtCompactLoopEndReference left, VtCompactLoopEndReference right)
    {
        return Equals(left, right);
    }
}
