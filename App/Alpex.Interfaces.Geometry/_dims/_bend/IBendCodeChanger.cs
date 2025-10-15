namespace Alpex.Interfaces.Geometry;

/// <summary>
///     Potrafi wygenerować kod produktu dla zmienionego kąta
/// </summary>
public interface IBendCodeChanger
{
    string GetCodeForDifferentAngle(AngleDeg angle);
    string GetCodeForDifferentArms(ArmLengths arms);
}
