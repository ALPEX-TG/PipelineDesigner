using System.ComponentModel;

namespace Alpex.Interfaces.Common;

/// <summary>
/// Rodzaj modelu 3D, który może zaprezentować trójnik
/// </summary>
public enum TeePresenationKind
{
    /// <summary>
    /// Wznośny
    /// </summary>
    [Description("Wznośny")]
    Rising,

    /// <summary>
    /// Opadający
    /// </summary>
    [Description("Opadowy")]
    Drooping,

    /// <summary>
    /// Równoległy
    /// </summary>
    [Description("Równoległy")]
    Parallel,

    /// <summary>
    /// Płaski
    /// </summary>
    [Description("Płaski")]
    Flat
}
