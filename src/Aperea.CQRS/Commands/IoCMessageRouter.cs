using System;
using System.Collections.Concurrent;
using System.Reflection;
using Aperea.CQRS.Bus.Direct;
using StructureMap;

namespace Aperea.CQRS.Commands
{
    public class IoCMessageRouter : IRouteMessages
    {
        static readonly ConcurrentDictionary<Type, MethodInfo> MethodInfos = new ConcurrentDictionary<Type, MethodInfo>();
        
        private readonly IContainer _container;
        public IoCMessageRouter(IContainer container)
        {
            _container = container;
        }

        public void Route(object message)
        {
            var commandHandlerType = typeof (ICommandHandler<>).MakeGenericType(message.GetType());
            var methodInfo = MethodInfos.GetOrAdd(commandHandlerType, t => t.GetMethod("Execute",new []{message.GetType()}));
            var instances = _container.GetAllInstances(commandHandlerType);
            foreach (var instance in instances)
            {
                methodInfo.Invoke(instance, new[] { message });
            }
        }
    }
}