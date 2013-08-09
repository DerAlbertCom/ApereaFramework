namespace Aperea.Common
{
    public interface ICommand
    {
    }

    public interface ICommand<TResult> : ICommand
    {
        TResult Result { get; set; }
    }
}