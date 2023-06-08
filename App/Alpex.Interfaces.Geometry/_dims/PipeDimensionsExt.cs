namespace Alpex.Interfaces.Geometry;

public static class PipeDimensionsExt
{
    /// <summary>
    ///     Promień zewnętrzny pianki
    /// </summary>
    /// <param name="x"></param>
    /// <returns></returns>
    public static double GetFoamOuterRadius(this IPipeDimensions x)
    {
        return x.GetRzp() - x.PlasticCoveringThickness;
    }

    public static double GetGruboscRury(this IPipeDimensions x)
    {
        return x.SteelThickness;
    }

    public static double GetRn(this IPipeDimensions x)
    {
        return x.Dn * 0.5;
    }

    public static double GetRz(this IPipeDimensions x)
    {
        return x.Dz * 0.5;
    }

    public static double GetRzp(this IPipeDimensions x)
    {
        return x.Dzp * 0.5;
    }

    public static double GetWireMargin(this IPipeDimensions x)
    {
        return x.NotCoveredMargin * 0.3;
    }

    public static double GetWireR(this IPipeDimensions x)
    {
        return x.GetRz() + 0.5 * (x.GetFoamOuterRadius() - x.GetRz());
    }
}
