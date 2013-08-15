using System;
using Microsoft.Practices.ServiceLocation;

namespace Aperea.Commands
{
    public class QueryDispatcher : IQueryDispatcher
    {
        private readonly IServiceLocator locator;

        public QueryDispatcher(IServiceLocator locator)
        {
            this.locator = locator;
        }

        public IQueryHandler GetHandler(IQueryCommand query)
        {
            var interfaces = query.GetType().GetInterfaces();
            foreach (var type in interfaces)
            {
                if (type.IsGenericType && typeof (IQueryCommand).IsAssignableFrom(type))
                {
                    var handlerType = typeof (IQueryHandler<,>).MakeGenericType(query.GetType(),
                        type.GetGenericArguments()[0]);
                    return (IQueryHandler) locator.GetInstance(handlerType);
                }
            }
            throw new InvalidOperationException("don't implement IQueryCommand directory, use IQueryCommand<T>");
        }
    }
}