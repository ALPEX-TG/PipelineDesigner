namespace Alpex.Interfaces.Geometry;

/// <summary>
///     This interface represents pipe dimmension in meters
/// </summary>
public interface IDoublePipeDimensions : IPipeDimensions
{
    /// <summary>
    ///     Distance between pipes - 'S' in PDF documents
    /// </summary>
    double SteelPipesGap { get; }
}
