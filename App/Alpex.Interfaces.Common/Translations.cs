using iSukces.Translation;

namespace Alpex.Interfaces.Common;

public static class Translations1
{
    static Translations1()
    {
        StrUnableToConvertParIntoDate.Value = "Nie przekonwertować '{0}' na datę.";
    }

    /// <summary>
    ///     Text: Nie przekonwertować '{0}' na datę.
    /// </summary>
    public static readonly LiteLocalTextSource StrUnableToConvertParIntoDate =
        new LiteLocalTextSource("Pd.Common.UnableToConvertParIntoDate");
}
