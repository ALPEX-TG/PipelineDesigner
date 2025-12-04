using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using iSukces.Mathematics;
#if WPF
using System.Windows.Media.Media3D;
#else
using iSukces.Mathematics.Compatibility;
#endif
namespace Alpex.Interfaces.Geometry;

[ImmutableObject(true)]
public sealed partial class PipelineEndingInfo
{
    public PipelineEndingInfo(IReadOnlyList<EndingInfo3D>? steelPipes, IReadOnlyList<EndingInfo3D>? wires)
    {
        SteelPipes = steelPipes ?? Array.Empty<EndingInfo3D>();
        Wires      = wires ?? Array.Empty<EndingInfo3D>();
    }

    public static PipelineEndingInfo? operator /(PipelineEndingInfo? obj, Coordinates3D? coordinates)
    {
        if (obj is null)
            return null;
        if (coordinates is null)
            return obj;
        coordinates = coordinates.Reversed;
        return new PipelineEndingInfo(
            Transform(obj.SteelPipes, coordinates),
            Transform(obj.Wires, coordinates));
    }

    public static PipelineEndingInfo? operator *(PipelineEndingInfo? obj, Coordinates3D? coordinates)
    {
        if (obj is null)
            return null;
        if (coordinates is null)
            return obj;
        return new PipelineEndingInfo(
            Transform(obj.SteelPipes, coordinates),
            Transform(obj.Wires, coordinates));
    }

    private static EndingInfo3D[] Transform(IReadOnlyList<EndingInfo3D>? list, Coordinates3D coordinates)
    {
        if (list is null || list.Count == 0)
            return Array.Empty<EndingInfo3D>();

        var count  = list.Count;
        var result = new EndingInfo3D[count];
        for (var i = count - 1; i >= 0; i--)
            result[i] = list[i] * coordinates;
        return result;
    }

    /// <summary>
    /// </summary>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public EndingInfo3D SteelPipe()
    {
        // todo Ta metoda jest tak długo dobra jak nie mamy rur podwójnych
        return SteelPipes[0];
    }

    /// <summary>
    /// </summary>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Point3D SteelPipePoint()
    {
        // todo Ta metoda jest tak długo dobra jak nie mamy rur podwójnych
        return SteelPipe().Point;
    }

    public override string ToString()
    {
        var o = SteelPipe();
        return $"Ending {o.Point}, => {o.Direction}";
    }

    public IReadOnlyList<EndingInfo3D> SteelPipes { get; }

    public IReadOnlyList<EndingInfo3D> Wires { get; }
}
#if WPF
partial class PipelineEndingInfo
{
    public static PipelineEndingInfo? operator /(PipelineEndingInfo? obj, Matrix3D matrix)
    {
        if (obj is null)
            return null;
        matrix.Invert();
        return new PipelineEndingInfo(
            Transform(obj.SteelPipes, matrix),
            Transform(obj.Wires, matrix));
    }

    public static PipelineEndingInfo? operator *(PipelineEndingInfo? obj, Matrix3D matrix)
    {
        if (obj is null)
            return null;
        return new PipelineEndingInfo(
            Transform(obj.SteelPipes, matrix),
            Transform(obj.Wires, matrix));
    }


    private static IReadOnlyList<EndingInfo3D> Transform(IReadOnlyList<EndingInfo3D>? list, Matrix3D matrix)
    {
        if (list is null || list.Count == 0)
            return Array.Empty<EndingInfo3D>();

        var count  = list.Count;
        var result = new EndingInfo3D[count];
        for (var i = count - 1; i >= 0; i--)
        {
            result[i] = list[i] * matrix;
        }

        return result;
    }
}
#endif