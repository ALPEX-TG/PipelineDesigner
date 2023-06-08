namespace Alpex.Interfaces.Geometry;

public interface ITeeDimensions
{
    IPipeDimensions Dims1    { get; set; }
    IPipeDimensions DimsFork { get; set; }
    IPipeDimensions Dims2    { get; set; }

    double B { get; set; }
    double H { get; set; }
    double L { get; set; }

    double  Radius              { get; set; }
    Radians Angle1              { get; set; }
    Radians ForkHorizontalAngle { get; set; }
}
