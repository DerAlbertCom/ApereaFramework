using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Threading;
using Microsoft.Practices.ServiceLocation;

namespace Aperea.Messaging
{
    public class InMemoryMessageBus : IMessageBus
    {
        private readonly IServiceLocator locator;
        private readonly SynchronizationContext threadContext;

        public InMemoryMessageBus(IServiceLocator locator)
        {
            this.locator = locator;
            threadContext = AsyncOperationManager.SynchronizationContext;
        }

        public void Publish(object message)
        {
            if (message == null)
            {
                throw new ArgumentNullException("message");
            }
            threadContext.Post(DispatchMessage, message);
        }

        public void Send(object message)
        {
            if (message == null)
            {
                throw new ArgumentNullException("message");
            }
            DispatchMessage(message);
        }

        private void DispatchMessage(object message)
        {
            Type handlerType = typeof (IHandler<>);
            handlerType = handlerType.MakeGenericType(message.GetType());
            MethodInfo handleMethod = handlerType.GetMethod("Handle");

            IEnumerable<object> handlers = locator.GetAllInstances(handlerType);
            foreach (object handler in handlers)
            {
                handleMethod.Invoke(handler, new[] {message});
            }
        }
    }
}