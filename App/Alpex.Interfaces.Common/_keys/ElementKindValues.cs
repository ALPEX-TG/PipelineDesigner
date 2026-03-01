using System;

namespace Alpex.Interfaces.Common;

public static class ElementKindValues
{
    /// <summary>
    /// Rura
    /// </summary>
    public static readonly Guid Pipe = Guid.Parse("{60C2B125-6629-426B-B174-F14C1504E673}");

    /// <summary>
    /// Trójnik
    /// </summary>
    public static readonly Guid Tee = Guid.Parse("{8ACEE336-8968-429B-90D4-FFEEA24CCDA6}");

    /// <summary>
    /// Kolano preizolowane
    /// </summary>
    public static readonly Guid Elbow = Guid.Parse("{4D4974AF-13EE-4309-9CF8-DF8C619AC2D0}");

    /// <summary>
    /// Zwężka preizolowana
    /// </summary>
    public static readonly Guid Orifice = Guid.Parse("{F5446F4B-A669-4AE9-9EEA-0A7491CEC210}");

    /// <summary>
    /// Zawór
    /// </summary>
    public static readonly Guid Valve = Guid.Parse("{2088F990-9A4B-4875-9FBC-F680B2E81F0D}");

    /// <summary>
    /// Mufa kolanowa
    /// </summary>
    public static readonly Guid ElbowMuff = Guid.Parse("{5D55B2A0-CB12-4D53-B9E8-E138AF51F2D9}");

    /// <summary>
    /// Mufa redukcyjna
    /// </summary>
    public static readonly Guid ReductionMuff = Guid.Parse("4D3A742C-EEA8-463D-8BFF-80C3FA3D9287");

    /// <summary>
    /// Punkt stały
    /// </summary>
    public static readonly Guid FixedPoint = Guid.Parse("9C009193-99AD-431A-A5A0-D51597637710");

    /// <summary>
    /// Kompensator
    /// </summary>
    public static readonly Guid Compensator = Guid.Parse("C56B5E72-D84F-4C16-BA8C-5B5FABCED659");

    /// <summary>
    /// Redukcja stalowa
    /// </summary>
    public static readonly Guid SteelReduction = Guid.Parse("EF8268CE-DBC4-4A1A-B165-B4CD0A62CF0A");

    /// <summary>
    /// Komplet: redukcja stalowa + mufa redukcyjna
    /// </summary>
    public static readonly Guid ReductionMuffComplete = Guid.Parse("080591FB-126A-4182-A643-C6D23E5E39B9");

}
