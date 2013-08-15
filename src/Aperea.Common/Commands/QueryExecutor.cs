using System;
using Microsoft.Practices.ServiceLocation;

namespace Aperea.Commands
{
    [UsedImplicitly]
    public class QueryExecutor : IQueryExecutor
    {
        private readonly IQueryDispatcher dispatcher;

        private static readonly Lazy<IQueryExecutor> TheExecutor =
            new Lazy<IQueryExecutor>(() => ServiceLocator.Current.GetInstance<IQueryExecutor>());

        public static TResult Query<TResult>(IQueryCommand<TResult> command)
        {
            return TheExecutor.Value.ExecuteQuery(command);
        }

        public QueryExecutor(IQueryDispatcher dispatcher)
        {
            this.dispatcher = dispatcher;
        }

        public TResult ExecuteQuery<TResult>(IQueryCommand<TResult> command)
        {
            var handler = dispatcher.GetHandler(command);
            if (handler == null) 
                throw new ArgumentNullException("handler");
            return (TResult) handler.Query(command);
        }
    }
}