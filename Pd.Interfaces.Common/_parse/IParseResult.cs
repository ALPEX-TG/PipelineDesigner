namespace Pd.Interfaces.Common;

public interface IParseResult : IResult
{
    object GetValue();
}

public interface IParseResult<out TValue> : IResult
{
    TValue Value { get; }
}

public interface IParseResult<out TValue, out TStatus> : IParseResult<TValue>
{
    TStatus Status { get; }
}

public static class ParseResultExt
{
    public static bool HasError(this IResult parseResult)
    {
        return !string.IsNullOrEmpty(parseResult.Error);
    }

    public static bool IsOk(this IResult parseResult)
    {
        return string.IsNullOrEmpty(parseResult.Error);
    }
}
