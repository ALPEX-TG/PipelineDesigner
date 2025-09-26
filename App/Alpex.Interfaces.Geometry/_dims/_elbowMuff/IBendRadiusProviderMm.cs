namespace Alpex.Interfaces.Geometry;

public interface IBendRadiusProviderMm
{
    
    /// <summary>
    ///     Niestandardowy promień gięcia w mm
    /// </summary>
    double? BendRadius { get; }

    
    /// <summary>
    ///     Domyslny promień gięcia w mm
    /// </summary>
    double DefaultBendRadius { get; }

}

public static class BendRadiusProviderMmExtensions
{
    public static SimpleLength GetEffectiveBendRadius(this IBendRadiusProviderMm radiusProviderMm)
    {
        return SimpleLength.Mm(radiusProviderMm.BendRadius ?? radiusProviderMm.DefaultBendRadius);
    }
}

