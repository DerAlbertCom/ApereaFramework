using System.Web.Hosting;
using Aperea.Infrastructure.Registration;
using Aperea.MVC.PortableAreas;
using StructureMap;
using StructureMap.Configuration.DSL;
using StructureMap.Pipeline;

namespace Aperea.MVC.Initialize
{
    public class ApereaMvcRegistry : Registry
    {
        public ApereaMvcRegistry()
        {
            Scan(x =>
            {
                x.AssembliesFromApplicationBaseDirectory(StructureMapAssemblyFilter.Filter);
                x.AddAllTypesOf<VirtualPathProvider>();
                x.With(new RemoteActionWorkerConvention());

                For<IPortableArea>().LifecycleIs(Lifecycles.GetLifecycle(InstanceScope.Singleton)).Use<PortableArea>();
            });
        }
    }
}