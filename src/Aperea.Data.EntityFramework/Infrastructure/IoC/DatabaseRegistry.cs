using Aperea.Data;
using StructureMap.Configuration.DSL;

namespace Aperea.Infrastructure.IoC
{
    public class DatabaseRegistry : Registry
    {
        public DatabaseRegistry()
        {
            For(typeof (IRepository<>)).HybridHttpOrThreadLocalScoped().Use(typeof (Repository<>));
            For(typeof (IQuery<>)).HybridHttpOrThreadLocalScoped().Use(typeof (Query<>));
            For<IDatabaseContext>().HybridHttpOrThreadLocalScoped().Use<DatabaseContext>();
        }
    }
}