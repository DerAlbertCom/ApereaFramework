using System.Web.Mvc;
using Aperea.Infrastructure.Bootstrap;

namespace Aperea.MVC.Views
{
    public class ViewEnginesInitialize : IBootstrapItem
    {
        public void Execute()
        {
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.AddIPhone<RazorViewEngine>();
            ViewEngines.Engines.AddIPad<RazorViewEngine>();
            ViewEngines.Engines.AddGenericMobile<RazorViewEngine>();
            ViewEngines.Engines.Add(new RazorViewEngine());
        }

        public int Order
        {
            get { return 0; }
        }
    }
}