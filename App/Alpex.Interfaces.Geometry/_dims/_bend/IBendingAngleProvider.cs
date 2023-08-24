namespace Alpex.Interfaces.Geometry;

/// <summary>
/// Dostarcza informacji o kącie zagięcia np. dla kolana lub mufy kolanowej
/// </summary>
public interface IBendingAngleProvider
{
    /// <summary>
    /// Kąt zagięcia dla kolana, mufy kolanowej itp.
    /// </summary>
    AngleDeg Angle { get; }   
}
