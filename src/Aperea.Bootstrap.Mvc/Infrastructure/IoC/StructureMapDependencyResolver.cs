using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using StructureMap;

namespace Aperea.Infrastructure.IoC
{
    public class StructureMapDependencyResolver : IDependencyResolver
    {
        readonly IContainer container;

        public StructureMapDependencyResolver(IContainer container)
        {
            this.container = container;
        }

        public object GetService(Type serviceType)
        {
            return container.TryGetInstance(serviceType) ?? AddConcreteServiceTypeToContainer(serviceType);
        }

        object AddConcreteServiceTypeToContainer(Type serviceType)
        {
            if (serviceType.IsAbstract)
            {
                return null;
            }
            container.Configure(x => x.For(serviceType).Use(serviceType));
            return container.TryGetInstance(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return container.GetAllInstances(serviceType).Cast<object>();
        }
    }
}