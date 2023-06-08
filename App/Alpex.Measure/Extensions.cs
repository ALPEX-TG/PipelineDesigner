using System.Globalization;
using System.Runtime.CompilerServices;

namespace Alpex.Measure;

internal static class Extensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int ParseIntInvariant(this string x)
    {
        return int.Parse(x, NumberStyles.Any, CultureInfo.InvariantCulture);
    }

    internal static string ToInvariantString(this int x)
    {
        return x.ToString(CultureInfo.InvariantCulture);
    }
}
