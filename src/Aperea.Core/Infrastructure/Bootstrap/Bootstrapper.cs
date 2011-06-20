using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.ServiceLocation;
using StructureMap;

namespace Aperea.Infrastructure.Bootstrap
{
    public class Bootstrapper
    {
        readonly List<IBootstrapItem> _bootstrapItems;

        Bootstrapper()
        {
            _bootstrapItems = new List<IBootstrapItem>();
        }

        public static Bootstrapper Start()
        {
            return new Bootstrapper();
        }

        public Bootstrapper FromDependencyResolver()
        {
            var instances = ServiceLocator.Current.GetAllInstances<IBootstrapItem>();
            _bootstrapItems.AddRange(instances);
            return this;
        }

        public void Execute()
        {
            foreach (var bootstrapItem in _bootstrapItems)
            {
                bootstrapItem.Execute();
            }
        }
    }
}