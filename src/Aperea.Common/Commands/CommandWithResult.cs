namespace Aperea.Commands
{
    public abstract class CommandWithResult<TResult>: ICommand<TResult>
    {
        public TResult Result { get; set; }
    }
}