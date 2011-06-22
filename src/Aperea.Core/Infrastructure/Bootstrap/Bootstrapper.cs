using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.ServiceLocation;

namespace Aperea.Infrastructure.Bootstrap
{
    public class Bootstrapper
    {
        readonly List<IBootstrapItem> _bootstrapItems;

        Bootstrapper()
        {
            _bootstrapItems = new List<IBootstrapItem>();
        }

        public static void Start()
        {
            new Bootstrapper().FromDependencyResolver().Execute();
        }

        Bootstrapper FromDependencyResolver()
        {
            var instances = ServiceLocator.Current.GetAllInstances<IBootstrapItem>();
            _bootstrapItems.AddRange(instances.OrderBy(b=>b.Order));
            return this;
        }

        void Execute()
        {
            foreach (var bootstrapItem in _bootstrapItems)
            {
                bootstrapItem.Execute();
            }
        }
    }
}