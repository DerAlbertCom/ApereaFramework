using System;
using Aperea.MVC.Attributes;
using Aperea.MVC.RemoteActions;
using StructureMap.Configuration.DSL;
using StructureMap.Graph;

namespace ApereaStart.Initialize
{
    internal class RemoteActionWorkerConvention : IRegistrationConvention
    {
        public void Process(Type type, Registry registry)
        {
            if (!type.IsAbstract && typeof (IRemoteActionWorker).IsAssignableFrom(type))
            {
                RemoteActionNameAttribute nameAttribute = GetRemoteActionNameAttribute(type);
                registry.AddType(typeof (IRemoteActionWorker), type, nameAttribute.WebActionName);
            }
        }

        RemoteActionNameAttribute GetRemoteActionNameAttribute(Type type)
        {
            var attributes = type.GetCustomAttributes(typeof (RemoteActionNameAttribute), false);
            if (attributes.Length == 0)
            {
                throw new InvalidOperationException(string.Format("{0} has no RemoteActionNameAttribute", type.FullName));
            }
            return (RemoteActionNameAttribute) attributes[0];
        }
    }
}