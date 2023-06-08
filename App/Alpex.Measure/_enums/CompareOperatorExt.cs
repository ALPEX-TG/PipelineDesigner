using System;
using System.Collections.Generic;

namespace Alpex.Measure;

public static class CompareOperatorExt
{
    static CompareOperatorExt()
    {
        ParseDictionary = new Dictionary<string, MeasureStatus>
        {
            [""]                                                  = MeasureStatus.Empty,
            [MeasureStatus.GreaterThan.FriendlyPrefix()]          = MeasureStatus.GreaterThan,
            [MeasureStatus.GreaterThanOrEqualTo.FriendlyPrefix()] = MeasureStatus.GreaterThanOrEqualTo,
            [MeasureStatus.LessThan.FriendlyPrefix()]             = MeasureStatus.LessThan,
            [MeasureStatus.LessThanOrEqualTo.FriendlyPrefix()]    = MeasureStatus.LessThanOrEqualTo,

            [MeasureStatus.NotMeasurable.FriendlyPrefix()] = MeasureStatus.NotMeasurable,
            [MeasureStatus.Approximately.FriendlyPrefix()] = MeasureStatus.Approximately,

            [">="] = MeasureStatus.GreaterThanOrEqualTo,
            ["<="] = MeasureStatus.LessThanOrEqualTo
        };
    }

    public static string FriendlyPrefix(this MeasureStatus x)
    {
        switch (x)
        {
            case MeasureStatus.None:
            case MeasureStatus.Empty:
                return "";
            case MeasureStatus.GreaterThan:
                return ">";
            case MeasureStatus.GreaterThanOrEqualTo:
                return "≥";
            case MeasureStatus.LessThan:
                return "<";
            case MeasureStatus.LessThanOrEqualTo:
                return "≤";
            case MeasureStatus.Approximately:
                return "≈";
            case MeasureStatus.NotMeasurable:
                return "?";
            default:
                throw new ArgumentOutOfRangeException(nameof(x), x, null);
        }
    }

    public static bool TryParse(string? prefix, out MeasureStatus oper)
    {
        prefix = prefix?.Trim();
        if (!string.IsNullOrEmpty(prefix))
            return ParseDictionary.TryGetValue(prefix, out oper);
        oper = MeasureStatus.None;
        return true;
    }

    private static readonly IReadOnlyDictionary<string, MeasureStatus> ParseDictionary;
}
