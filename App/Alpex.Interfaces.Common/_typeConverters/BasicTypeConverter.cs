using System;
using System.ComponentModel;
using System.Globalization;

namespace Alpex.Interfaces.Common;

public abstract class BasicTypeConverter<T> : TypeConverter
{
    public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
    {
        return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
    }

    public override bool CanConvertTo(ITypeDescriptorContext? context, Type? destinationType)
    {
        return destinationType == typeof(string) || base.CanConvertTo(context, destinationType);
    }

    public override object ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
    {
        var text = value as string;
        if (string.IsNullOrEmpty(text))
            return base.ConvertFrom(context, culture, value);
        return ConvertFromInternal(context, culture, text);
    }

    protected abstract T ConvertFromInternal(ITypeDescriptorContext context, CultureInfo culture, string value);

    public override object ConvertTo(ITypeDescriptorContext? context, CultureInfo? culture, object? value,
        Type destinationType)
    {
        if (destinationType == typeof(string) && value is T)
            return ConvertToInternal(context, culture, (T)value, destinationType);
        return base.ConvertTo(context, culture, value, destinationType);
    }

    protected abstract string ConvertToInternal(ITypeDescriptorContext context, CultureInfo culture, T value,
        Type destinationType);
}
