using System.Collections.Generic;
using System.Web.Hosting;
using Aperea.Infrastructure.Bootstrap;

namespace Aperea.MVC.PortableAreas
{
    public class PortableAreaInitialize : IBootstrapItem
    {
        readonly IEnumerable<VirtualPathProvider> _virtualPathProviders;

        public PortableAreaInitialize(IEnumerable<VirtualPathProvider> virtualPathProviders)
        {
            _virtualPathProviders = virtualPathProviders;
        }

        public void Execute()
        {
            foreach (var virtualPathProvider in _virtualPathProviders)
            {
                HostingEnvironment.RegisterVirtualPathProvider(virtualPathProvider);
            }
        }

        public int Order
        {
            get { return 0; }
        }
    }
}