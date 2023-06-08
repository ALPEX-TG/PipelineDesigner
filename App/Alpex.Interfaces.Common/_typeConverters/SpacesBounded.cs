namespace Alpex.Interfaces.Common;

public sealed class SpacesBounded
{
    private SpacesBounded(int spacesBefore, int spacesAfter, string? text)
    {
        SpacesBefore = spacesBefore;
        SpacesAfter  = spacesAfter;
        Text         = text ?? string.Empty;
    }

    public static SpacesBounded Parse(string x)
    {
        if (string.IsNullOrEmpty(x))
            return new SpacesBounded(0, 0, x);

        var l1 = x.Length;
        x = x.TrimStart();
        var l2 = x.Length;
        x = x.TrimEnd();
        var l3 = x.Length;
        return new SpacesBounded(l1 - l2, l2 - l3, x);
    }

    public int    SpacesBefore { get; }
    public int    SpacesAfter  { get; }
    public string Text         { get; }
}
