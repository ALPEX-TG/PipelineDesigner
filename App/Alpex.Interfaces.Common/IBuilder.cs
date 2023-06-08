namespace Alpex.Interfaces.Common;

public interface IBuilder<out TResult>
{
    TResult Build();
}
