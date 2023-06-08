using System;
using System.ComponentModel;
using System.Globalization;
using Alpex.Interfaces.Common;

namespace Alpex.Interfaces.Geometry;

public sealed class ScaleTypeConverter : BasicTypeConverter<Scale>
{
    protected override Scale ConvertFromInternal(ITypeDescriptorContext context, CultureInfo culture, string value)
    {
        return Scale.Parse(value, culture.NumberFormat).GetValueOrThrow();
    }

    protected override string ConvertToInternal(ITypeDescriptorContext context, CultureInfo culture, Scale value,
        Type destinationType)
    {
        return value.ToString(culture);
    }
}
