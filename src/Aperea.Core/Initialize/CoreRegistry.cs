using Aperea.Data;
using Aperea.Infrastructure.Registration;
using StructureMap.Configuration.DSL;

namespace Aperea.Initialize
{
    public class CoreRegistry : Registry
    {
        public CoreRegistry()
        {
            Scan(c=>
            {
                c.AssembliesFromApplicationBaseDirectory(StructureMapAssemblyFilter.Filter);
                c.AddAllTypesOf<IDatabaseSeeder>();
                c.AddAllTypesOf<IDatabaseModelBuilder>();
            });
        }
    }
}