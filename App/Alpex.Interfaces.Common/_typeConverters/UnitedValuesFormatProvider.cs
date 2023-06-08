using System;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using JetBrains.Annotations;

namespace Alpex.Interfaces.Common;

public sealed class UnitedValuesFormatProvider
{
    private UnitedValuesFormatProvider(int spacesBefore, int spacesAfter, string startingText,
        string unitText,
        string endingText)
    {
        SpacesBefore = spacesBefore;
        SpacesAfter  = spacesAfter;
        StartingText = startingText;
        UnitText     = unitText;
        EndingText   = endingText;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void AddSpaces(int spaces, StringBuilder sb)
    {
        if (spaces >= 0)
            sb.Append(new string(' ', spaces));
    }

    [CanBeNull]
    public static UnitedValuesFormatProvider TryParse(string x)
    {
        if (string.IsNullOrEmpty(x))
            return null;
        var a = SpacesBounded.Parse(x);

        var m = FilterRegex.Match(a.Text);
        if (m.Success)
            return new UnitedValuesFormatProvider(a.SpacesBefore, a.SpacesAfter,
                m.Groups[1].Value,
                m.Groups[2].Value,
                m.Groups[3].Value);
        return new UnitedValuesFormatProvider(a.SpacesBefore, a.SpacesAfter, a.Text, null, null);
    }

    public string Format(IFormattable d, string unit, IFormatProvider invariantCulture)
    {
        var sb = new StringBuilder();
        AddSpaces(SpacesBefore, sb);

        var str = StartingText;
        if (!string.IsNullOrEmpty(str))
            sb.Append(d.ToString(str, invariantCulture));

        str = UnitText;
        if (!string.IsNullOrEmpty(str))
            sb.Append(str.Replace("$", unit));

        str = EndingText;
        if (!string.IsNullOrEmpty(str))
            sb.Append(d.ToString(str, invariantCulture));

        AddSpaces(SpacesAfter, sb);
        return sb.ToString();
    }

    public int    SpacesBefore { get; }
    public int    SpacesAfter  { get; }
    public string StartingText { get; }
    public string UnitText     { get; }
    public string EndingText   { get; }

    private const string FilterFilter = @"^(.*[^\s])?(\s*\$\s*)([^\s].*)?$";

    private static readonly Regex FilterRegex =
        new Regex(FilterFilter, RegexOptions.Multiline | RegexOptions.Compiled);
}
