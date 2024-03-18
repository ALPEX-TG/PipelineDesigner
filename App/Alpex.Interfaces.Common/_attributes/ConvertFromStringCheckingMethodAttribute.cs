/*
using System;
using JetBrains.Annotations;

namespace Alpex.Interfaces.Common;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Property)]
public sealed class ConvertFromStringCheckingMethodAttribute : Attribute
{
    public ConvertFromStringCheckingMethodAttribute(Type type, string staticMethodName)
    {
        Type             = type;
        StaticMethodName = staticMethodName;
    }

    public ConvertFromStringCheckingMethodAttribute(string staticMethodName)
    {
        StaticMethodName = staticMethodName;
    }

    /// <summary>
    ///     Jeśli jest null to weź reflected type
    /// </summary>
    [CanBeNull]
    public Type Type { get; }

    public string StaticMethodName { get; }
}
*/
