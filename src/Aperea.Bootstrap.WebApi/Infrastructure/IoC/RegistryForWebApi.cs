using StructureMap;

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