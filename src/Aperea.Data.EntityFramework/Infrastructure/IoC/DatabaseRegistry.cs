using Aperea.Data;
using StructureMap;
using StructureMap.Configuration.DSL;
using StructureMap.Web;

namespace Aperea.Infrastructure.IoC
{
    public class DatabaseRegistry : Registry
    {
        public DatabaseRegistry()
        {
            For(typeof (IRepository<>), WebLifecycles.Hybrid).Use(typeof (Repository<>));
            For(typeof (IQuery<>), WebLifecycles.Hybrid).Use(typeof (Query<>));
            For<IDatabaseContext>().HybridHttpOrThreadLocalScoped().Use<DatabaseContext>();
        }
    }
}