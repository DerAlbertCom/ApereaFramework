using System;
using System.Collections.Generic;
using System.Linq;
using Aperea.Infrastructure.IoC;
using Microsoft.Practices.ServiceLocation;

namespace Aperea.Infrastructure.Bootstrap
{
    public class Bootstrapper
    {
        public static void Start()
        {
            RegisterStructureMap.Execute();
            new Bootstrapper().Execute();
        }

        void Execute()
        {
            var instances = GetAllInstances().OrderBy(b => b.Order);
            foreach (var bootstrapItem in instances)
            {
                bootstrapItem.Execute();
            }
        }

        public static void End()
        {
            var instances = GetAllInstances().OrderByDescending(b => b.Order);
            foreach (var bootstrapItem in instances)
            {
                bootstrapItem.Dispose();
            }
        }

        static IEnumerable<IBootstrapItem> GetAllInstances()
        {
            return ServiceLocator.Current.GetAllInstances<IBootstrapItem>();
        }
    }
}