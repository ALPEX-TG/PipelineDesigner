using System;
using Newtonsoft.Json;

namespace Pd.Interfaces.Common;

/// <summary>
///     UID elementu prefabrykowanego z alarmem w środku
/// </summary>
[CompactSerializer]
[JsonConverter(typeof(XAlarmContainerUidJsonConverter))]
public readonly struct XAlarmContainerUid : IGuidBasedKey, IEquatable<XAlarmContainerUid>
{
    public XAlarmContainerUid(Guid value)
    {
        Value = value;
    }

    public static XAlarmContainerUid NewId()
    {
        return new XAlarmContainerUid(Guid.NewGuid());
    }

    public static bool operator ==(XAlarmContainerUid left, XAlarmContainerUid right)
    {
        return left.Value == right.Value;
    }

    public static bool operator !=(XAlarmContainerUid left, XAlarmContainerUid right)
    {
        return left.Value != right.Value;
    }

    public static XAlarmContainerUid Parse(string guid)
    {
        return new XAlarmContainerUid(Guid.Parse(guid));
    }

    public bool Equals(XAlarmContainerUid other)
    {
        return Value.Equals(other.Value);
    }

    public override bool Equals(object? obj)
    {
        return obj is XAlarmContainerUid s && Value.Equals(s.Value);
    }

    public bool Equals(XAlarmContainerUid? obj)
    {
        return obj is not null && Value.Equals(obj.Value.Value);
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }

    public override string ToString()
    {
        return "AlarmContainer UID " + Value;
    }

    public string ValueAsText()
    {
        return IsEmpty ? "Empty" : Value.ToString();
    }

    public bool IsEmpty => Value.Equals(Guid.Empty);

    public static XAlarmContainerUid Empty => new XAlarmContainerUid(Guid.Empty);

    public Guid Value { get; }
}
