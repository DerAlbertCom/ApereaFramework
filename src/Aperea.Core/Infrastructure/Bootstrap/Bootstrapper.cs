using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.ServiceLocation;
using StructureMap;

namespace Aperea.Infrastructure.Bootstrap
{
    public class Bootstrapper
    {
        private readonly List<IBootstrapItem> bootstrapItems;

        private Bootstrapper()
        {
            bootstrapItems = new List<IBootstrapItem>();
        }

        public static Bootstrapper Start()
        {
            return new Bootstrapper();
        }

        public Bootstrapper FromDependencyResolver()
        {
            var instances = ServiceLocator.Current.GetAllInstances<IBootstrapItem>();
            bootstrapItems.AddRange(instances);
            return this;
        }

        public Bootstrapper With<T>() where T : IBootstrapItem
        {
            if (AlreadyAdded(typeof (T)))
            {
                throw new ArgumentException(string.Format("the type {0} is already added", typeof (T)));
            }
            bootstrapItems.Add(CreateInstance(typeof (T)));
            return this;
        }

        private bool AlreadyAdded(Type type)
        {
            return bootstrapItems.Any(bootstrapItem => bootstrapItem.GetType() == type);
        }

        public void Execute()
        {
            foreach (var bootstrapItem in bootstrapItems)
            {
                bootstrapItem.Execute();
            }
        }

        private static IBootstrapItem CreateInstance(Type type)
        {
            IBootstrapItem item;
            try
            {
                item = (IBootstrapItem) ServiceLocator.Current.GetInstance(type);
            }
            catch (StructureMapException)
            {
                item = (IBootstrapItem) Activator.CreateInstance(type);
            }
            return item;
        }
    }
}