using System;
using System.ComponentModel;
using System.Globalization;
using Pd.Interfaces.Common;

namespace Pd.Interfaces.Geometry;

public sealed class SimpleLengthTypeConverter : BasicTypeConverter<SimpleLength>
{
    protected override SimpleLength ConvertFromInternal(ITypeDescriptorContext context, CultureInfo culture,
        string value)
    {
        var len = Length.Parse(value, culture.NumberFormat).GetValueOrThrow();
        return SimpleLength.M((double)len.GetMeters());
    }

    protected override string ConvertToInternal(ITypeDescriptorContext context, CultureInfo culture,
        SimpleLength value, Type destinationType)
    {
        return value.ToString(culture);
    }
}
