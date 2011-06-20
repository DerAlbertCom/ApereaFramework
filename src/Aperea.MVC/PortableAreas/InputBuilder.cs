using System;
using System.Linq;
using System.Web.Hosting;
using System.Web.Mvc;
using Aperea.Infrastructure.Bootstrap;

namespace Aperea.MVC.PortableAreas
{
	public class InputBuilder : IBootstrapItem
	{
		private readonly Action<VirtualPathProvider> RegisterPathProvider = HostingEnvironment.RegisterVirtualPathProvider;

	    public void Execute()
	    {
            if (!ViewEngines.Engines.Any(engine => engine.GetType().Equals(typeof(InputBuilderViewEngine))))
            {
                VirtualPathProvider pathProvider = new AssemblyResourceProvider();

                RegisterPathProvider(pathProvider);

                var resourceStore = new AssemblyResourceStore(typeof(PortableAreaRegistration), "/areas", typeof(PortableAreaRegistration).Namespace);
                AssemblyResourceManager.RegisterAreaResources(resourceStore);

                ViewEngines.Engines.Add(new InputBuilderViewEngine(new string[0]));
            }
        }
	}
}