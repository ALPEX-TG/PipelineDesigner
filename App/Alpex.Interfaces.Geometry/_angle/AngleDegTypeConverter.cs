using System;
using System.ComponentModel;
using System.Globalization;
using Alpex.Interfaces.Common;

namespace Alpex.Interfaces.Geometry;

public sealed class AngleDegTypeConverter : BasicTypeConverter<AngleDeg>
{
    protected override AngleDeg ConvertFromInternal(ITypeDescriptorContext context, CultureInfo culture, string value)
    {
        return AngleDeg.Parse(value, culture.NumberFormat).GetValueOrThrow();
    }

    protected override string ConvertToInternal(ITypeDescriptorContext context, CultureInfo culture, AngleDeg value,
        Type destinationType)
    {
        return value.ToString(culture);
    }
}
