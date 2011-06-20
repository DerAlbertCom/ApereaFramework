using System;
using Aperea.MVC.Attributes;
using Aperea.MVC.RemoteActions;
using StructureMap.Configuration.DSL;
using StructureMap.Graph;

namespace Aperea.MVC.Initialize
{
    internal class RemoteActionWorkerConvention : IRegistrationConvention
    {
        public void Process(Type type, Registry registry)
        {
            if (!type.IsAbstract && typeof (IRemoteActionWorker).IsAssignableFrom(type))
            {
                RemoteActionNameAttribute nameAttribute = GetWebActionNameAttribute(type);
                registry.AddType(typeof (IRemoteActionWorker), type, nameAttribute.WebActionName);
            }
        }

        RemoteActionNameAttribute GetWebActionNameAttribute(Type type)
        {
            var attributes = type.GetCustomAttributes(typeof (RemoteActionNameAttribute), false);
            if (attributes.Length == 0)
            {
                throw new InvalidOperationException(string.Format("{0} has no WebActionNameAttribute", type.FullName));
            }
            return (RemoteActionNameAttribute) attributes[0];
        }
    }
}