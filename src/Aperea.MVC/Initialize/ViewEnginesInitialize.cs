using System.Web.Mvc;
using Aperea.Infrastructure.Bootstrap;
using Aperea.MVC.Views;

namespace Aperea.MVC.Initialize
{
    public class ViewEnginesInitialize : IBootstrapItem
    {
        public void Execute()
        {
            ViewEngines.Engines.Clear();
            //ViewEngines.Engines.AddIPhone<ResourceViewEngine>();
            //ViewEngines.Engines.AddIPad<ResourceViewEngine>();
            //ViewEngines.Engines.AddGenericMobile<ResourceViewEngine>();
            ViewEngines.Engines.Add(new ResourceViewEngine());
        }

        public int Order
        {
            get { return 0; }
        }
    }
}