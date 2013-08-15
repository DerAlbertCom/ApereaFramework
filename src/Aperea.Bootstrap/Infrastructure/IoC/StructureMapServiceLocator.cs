using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.ServiceLocation;
using StructureMap;

namespace Aperea.Infrastructure.IoC
{
    public class StructureMapServiceLocator : ServiceLocatorImplBase
    {
        readonly IContainer container;

        public StructureMapServiceLocator(IContainer container)
        {
            this.container = container;
        }

        protected override object DoGetInstance(Type serviceType, string key)
        {
            if (key == null)
                return container.GetInstance(serviceType);
            return container.GetInstance(serviceType, key);
        }

        protected override IEnumerable<object> DoGetAllInstances(Type serviceType)
        {
            return container.GetAllInstances(serviceType).Cast<object>();
        }
    }
}