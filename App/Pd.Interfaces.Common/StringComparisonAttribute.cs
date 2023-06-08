using System;

namespace Pd.Interfaces.Common;

[AttributeUsage(AttributeTargets.Property)]
public class StringComparisonAttribute : Attribute
{
    public StringComparisonAttribute(StringComparison comparison)
    {
        Comparison = comparison;
    }

    public StringComparison Comparison { get; }
}

public sealed class OrdinalIgnoreCaseAttribute : StringComparisonAttribute
{
    public OrdinalIgnoreCaseAttribute()
        : base(StringComparison.OrdinalIgnoreCase)
    {
    }
}

public sealed class CurrentCultureIgnoreCaseAttribute : StringComparisonAttribute
{
    public CurrentCultureIgnoreCaseAttribute()
        : base(StringComparison.CurrentCultureIgnoreCase)
    {
    }
}

public sealed class NullToStringEmptyAttribute : Attribute
{
}
