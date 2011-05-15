using Aperea.Infrastructure.Registration;
using Aperea.MVC.Infrastructure;
using StructureMap.Configuration.DSL;

namespace Aperea.MVC.Initialize
{
    public class ApereaMvcRegistry : Registry
    {
        public ApereaMvcRegistry()
        {
            Scan(x =>
                     {
                         x.AssembliesFromApplicationBaseDirectory(StructureMapAssemblyFilter.Filter);
                         x.With(new RemoteActionWorkerConvention());
                     });
        }
    }
}