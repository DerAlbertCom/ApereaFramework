using System.Web.Mvc;
using Aperea.Infrastructure.Bootstrap;
using Aperea.MVC.Views;

namespace ApereaStart.Initialize
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
    }
}