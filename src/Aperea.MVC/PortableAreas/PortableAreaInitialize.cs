using System.Web.Hosting;
using Aperea.Infrastructure.Bootstrap;

namespace Aperea.MVC.PortableAreas
{
    public class PortableAreaInitialize : IBootstrapItem
    {
        public void Execute()
        {
            HostingEnvironment.RegisterVirtualPathProvider(new AssemblyResourceProvider());
        }

        public int Order
        {
            get { return 0; }
        }
    }
}