using System;
using System.Linq;
using System.Reflection;

namespace Pd.Interfaces.Common;

public sealed class CompactSerializerAttribute : Attribute
{
    public CompactSerializerAttribute(string? serializeMethodOrProperty = null, string deserializeMethod = null)
    {
        SerializeMethodOrProperty = serializeMethodOrProperty;
        DeserializeMethod         = deserializeMethod;
    }

    public MethodInfo? GetDeserializationMethod(Type type)
    {
        if (string.IsNullOrEmpty(DeserializeMethod))
            return null;
        const BindingFlags bindingAttr = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static;
        var method = type.GetMethods(bindingAttr)
            .FirstOrDefault(a => a.Name == DeserializeMethod && a.GetParameters().Length == 1);
        return method;
    }

    public MethodInfo? GetSerializationMethod(Type type)
    {
        if (string.IsNullOrEmpty(SerializeMethodOrProperty))
            return null;
        const BindingFlags bindingAttr = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
        var method = type.GetMethods(bindingAttr)
            .FirstOrDefault(a => a.Name == SerializeMethodOrProperty && a.GetParameters().Length == 0);
        if (method != null)
            return method;

        var prope = type.GetProperty(SerializeMethodOrProperty);
        if (prope?.GetMethod != null)
            return prope.GetMethod;

        var msg =
            $"Unable to find suitable serialization method or property for type '{SerializeMethodOrProperty}' {type}";
        throw new Exception(msg);
    }

    public string? SerializeMethodOrProperty { get; }
    public string? DeserializeMethod         { get; }
}
