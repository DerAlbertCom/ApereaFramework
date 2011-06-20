using Aperea.Data;
using Aperea.Infrastructure.Data;
using Aperea.Infrastructure.Registration;
using Aperea.Repositories;
using StructureMap;
using StructureMap.Configuration.DSL;
using StructureMap.Pipeline;

namespace Aperea.Initialize
{
    public class CoreRegistry : Registry
    {
        public CoreRegistry()
        {
            Scan(c =>
            {
                c.AssembliesFromApplicationBaseDirectory(StructureMapAssemblyFilter.Filter);
                c.AddAllTypesOf<IDatabaseSeeder>();
                c.AddAllTypesOf<IModuleInfo>();
                c.AddAllTypesOf<IDatabaseModelBuilder>();
                c.AddAllTypesOf(typeof (IRepository<>));

                c.WithDefaultConventions();
                For<IDatabaseContext>()
                    .LifecycleIs(Lifecycles.GetLifecycle(InstanceScope.Hybrid));
            });
        }
    }
}