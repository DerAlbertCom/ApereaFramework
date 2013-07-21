using System.Web.Http;

namespace Aperea.Infrastructure.IoC
{
    public static class HttpConfigurationExtensions
    {
        public static void SetDependencyResolver(this HttpConfiguration configuration)
        {
            configuration.DependencyResolver = new WebApiDependencyResolver(RegisterStructureMap.Container);
        }
    }
}