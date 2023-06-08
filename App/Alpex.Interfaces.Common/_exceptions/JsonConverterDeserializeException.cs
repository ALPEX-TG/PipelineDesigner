using System;

namespace Alpex.Interfaces.Common;

public sealed class JsonConverterDeserializeException : Exception
{
    public JsonConverterDeserializeException(string cause, Type type)
        : base(Format(cause, type))
    {
    }

    private static string Format(string cause, Type type)
    {
        return $"Unable to convert {cause} into {type} object";
    }
}
