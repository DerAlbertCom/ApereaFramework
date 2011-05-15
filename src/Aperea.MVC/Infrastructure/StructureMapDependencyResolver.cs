using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using StructureMap;

namespace Aperea.MVC.Infrastructure
{
    public class StructureMapDependencyResolver : IDependencyResolver
    {
        private readonly IContainer _container;

        public StructureMapDependencyResolver(IContainer container)
        {
            _container = container;
        }

        public object GetService(Type serviceType)
        {
            return _container.TryGetInstance(serviceType) ?? AddConcreteServiceTypeToContainer(serviceType);
        }

        private object AddConcreteServiceTypeToContainer(Type serviceType)
        {
            if (serviceType.IsAbstract)
            {
                return null;
            }
            _container.Configure(x => x.For(serviceType).Use(serviceType));
            return _container.TryGetInstance(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _container.GetAllInstances(serviceType).Cast<object>();
        }
    }
}