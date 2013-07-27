using StructureMap.Configuration.DSL;

namespace Aperea.Infrastructure.IoC
{
    public class RegistryForWebApi : Registry
    {
        public RegistryForWebApi()
        {
            Scan(s =>
            {
                s.AssembliesForApplication();
                s.With(new WebApiControllerRegistrationConvention());
            });
        }
    }
}