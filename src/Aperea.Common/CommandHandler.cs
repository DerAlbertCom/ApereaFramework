namespace Aperea.Common
{
    public abstract class CommandHandler<T, TResult> : ICommandHandler<T,TResult> where T : ICommand<TResult>
    {
        void ICommandHandler<T>.Execute(T command)
        {
            Execute(command);
        }

        public abstract TResult Execute(T command);

        void ICommandHandler.Execute(ICommand command)
        {
            Execute((T) command);
        }
    }

    public abstract class CommandHandler<T> : ICommandHandler<T> where T : ICommand
    {
        public abstract void Execute(T command);

        void  ICommandHandler.Execute(ICommand command)
        {
            Execute((T)command);
        }
    }
}