using System.Linq;
using Aperea.Infrastructure.Data;
using Aperea.Infrastructure.Registration;
using Aperea.MVC.RemoteActions;
using Aperea.Repositories;
using ApereaStart.RemoteActions;
using StructureMap;
using StructureMap.Configuration.DSL;
using StructureMap.Pipeline;

namespace ApereaStart.Initialize
{
    public class WebContainerRegistry : Registry
    {
        public WebContainerRegistry()
        {
            Scan(x =>
                     {
                         x.AssembliesFromApplicationBaseDirectory(StructureMapAssemblyFilter.Filter);
                         x.AddAllTypesOf(typeof (IRepository<>));
                         x.WithDefaultConventions();
                     });

            For<IDatabaseContext>()
                .LifecycleIs(Lifecycles.GetLifecycle(InstanceScope.Hybrid));
        }

        private void RegisterWebActionWorkers()
        {
            var workers = from t in GetType().Assembly.GetTypes()
                          where t.IsClass && !t.IsAbstract && t.IsAssignableFrom(typeof (IRemoteActionWorker))
                          select t;
            foreach (var type in workers)
            {
            }
        }
    }
}