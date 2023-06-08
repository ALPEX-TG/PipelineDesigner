namespace Alpex.Interfaces.Geometry;

public interface IPipeDimensionsCreator
{
    /// <summary>
    ///     Tworzy pełne wymiary rury na podstawie zadanych wartości podstawowych
    ///     W obrębie producenta jest to wystarczające
    /// </summary>
    /// <param name="dn">w mm</param>
    /// <param name="dz">w mm</param>
    /// <param name="dzp">w mm</param>
    /// <returns></returns>
    IPipeDimensionsMm CreatePipeDimensions(double dn, double dz, double dzp);
}
