using System;
using System.ComponentModel.DataAnnotations;

namespace Pd.Interfaces.Common;

public readonly struct ParseResult<T> : IParseResult, IParseResult<T>
{
    private ParseResult(T value, string? error)
    {
        Value = value;
        Error = error;
    }

    public static ParseResult<T> NotOk(string error)
    {
        return new ParseResult<T>(default, error);
    }

    public static ParseResult<T> Ok(T value)
    {
        return new ParseResult<T>(value, null);
    }

    public static implicit operator ParseResult<T>(T src)
    {
        return new ParseResult<T>(src, null);
    }

    object IParseResult.GetValue()
    {
        return Value;
    }

    public T GetValueOrThrow()
    {
        if (HasError)
            throw new ValidationException(Error);
        return Value;
    }

    public ParseResult<TNew> Map<TNew>(Func<T, TNew> map)
    {
        if (HasError)
            return ParseResult<TNew>.NotOk(Error);
        return new ParseResult<TNew>(map(Value), null);
    }

    public bool HasError => !string.IsNullOrEmpty(Error);

    public string? Error { get; }

    public T Value { get; }
}
