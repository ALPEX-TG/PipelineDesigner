using iSukces.Mathematics;
#if WPF
using System.Windows.Media.Media3D;
#else
using iSukces.Mathematics.Compatibility;
#endif

namespace Alpex.Interfaces.Geometry;

public readonly struct EndingInfo3D
{
    public EndingInfo3D(Point3D point, Vector3D direction, object tag)
    {
        Point     = point;
        Direction = direction;
        Tag       = tag;
    }

    public EndingInfo3D(Point3D point, Vector3D direction)
    {
        Point     = point;
        Direction = direction;
        Tag       = null;
    }

    public static EndingInfo3D operator /(EndingInfo3D obj, Coordinates3D coordinates)
    {
        coordinates = coordinates.Reversed;
        var point     = obj.Point * coordinates;
        var direction = obj.Direction * coordinates;
        return new EndingInfo3D(point, direction, obj.Tag);
    }

    public static EndingInfo3D operator /(EndingInfo3D obj, Coord3D coordinates)
    {
        coordinates = coordinates.Reversed;
        var point     = obj.Point * coordinates;
        var direction = obj.Direction * coordinates;
        return new EndingInfo3D(point, direction, obj.Tag);
    }


    public static EndingInfo3D operator *(EndingInfo3D obj, Coordinates3D coordinates)
    {
        var point     = obj.Point * coordinates;
        var direction = obj.Direction * coordinates;
        return new EndingInfo3D(point, direction, obj.Tag);
    }

#if WPF
    public static EndingInfo3D operator *(EndingInfo3D obj, Matrix3D coordinates)
    {
        var point     = obj.Point * coordinates;
        var direction = obj.Direction * coordinates;
        return new EndingInfo3D(point, direction, obj.Tag);
    }
#endif

    public static EndingInfo3D operator *(EndingInfo3D obj, Coord3D coordinates)
    {
        var point     = obj.Point * coordinates;
        var direction = obj.Direction * coordinates;
        return new EndingInfo3D(point, direction, obj.Tag);
    }

    public override string ToString()
    {
        return $"Point: {Point}, Direction: {Direction}";
    }

    public Point3D  Point     { get; }
    public Vector3D Direction { get; }
    public object   Tag       { get; }
}