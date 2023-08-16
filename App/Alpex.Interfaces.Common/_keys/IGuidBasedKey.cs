using System;

namespace Alpex.Interfaces.Common;

public interface IGuidBasedKey
{
    #region Properties

    Guid Value { get; }

    #endregion
}
