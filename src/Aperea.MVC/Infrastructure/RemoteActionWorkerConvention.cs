using System;
using Aperea.MVC.Attributes;
using Aperea.MVC.RemoteActions;
using StructureMap.Configuration.DSL;
using StructureMap.Graph;

namespace Aperea.MVC.Infrastructure
{
    internal class RemoteActionWorkerConvention : IRegistrationConvention
    {
        public void Process(Type type, Registry registry)
        {
            if (!type.IsAbstract && typeof (IRemoteActionWorker).IsAssignableFrom(type))
            {
                WebActionNameAttribute nameAttribute = GetWebActionNameAttribute(type);
                registry.AddType(typeof (IRemoteActionWorker), type, nameAttribute.WebActionName);
            }
        }

        WebActionNameAttribute GetWebActionNameAttribute(Type type)
        {
            var attributes = type.GetCustomAttributes(typeof (WebActionNameAttribute), false);
            if (attributes.Length == 0)
            {
                throw new InvalidOperationException(string.Format("{0} has no WebActionNameAttribute", type.FullName));
            }
            return (WebActionNameAttribute) attributes[0];
        }
    }
}