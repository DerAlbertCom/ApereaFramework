using Aperea.Infrastructure.Bootstrap;
using Aperea.Infrastructure.Registration;

namespace ApereaStart.Initialize
{
    public static class ApplicationStart
    {
        public static void Initialize()
        {
            RegisterStructureMap.Execute();
            Bootstrapper.Start()
                .FromDependencyResolver()
                .Execute();
        }
    }
}