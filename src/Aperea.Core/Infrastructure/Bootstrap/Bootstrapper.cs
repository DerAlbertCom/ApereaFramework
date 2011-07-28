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