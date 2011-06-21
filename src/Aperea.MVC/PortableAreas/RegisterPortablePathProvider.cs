using System.Web.Hosting;
using System.Web.Mvc;
using Aperea.Infrastructure.Bootstrap;

namespace Aperea.MVC.PortableAreas
{
    public class RegisterPortablePathProvider : IBootstrapItem
    {
        public void Execute()
        {
            HostingEnvironment.RegisterVirtualPathProvider(new AssemblyResourceProvider());

            ViewEngines.Engines.Add(new InputBuilderViewEngine(new string[0]));
        }
    }
}