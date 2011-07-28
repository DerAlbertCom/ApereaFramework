using Aperea.Infrastructure.Bootstrap;
using Aperea.Infrastructure.Registration;

namespace Aperea.Initialize
{
    public static class ApplicationStart
    {
        public static void Initialize()
        {
            RegisterStructureMap.Execute();
            Bootstrapper.Start();
        }

        public static void End()
        {
            Bootstrapper.End();
        }
    }
}