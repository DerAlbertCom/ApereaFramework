using System.Web.Mvc;
using System.Web.Optimization;
using Aperea.Infrastructure.Bootstrap;

namespace Aperea.Initialize
{
    public class InitializeMvc : BootstrapItem
    {
        public override void Execute()
        {
            AreaRegistration.RegisterAllAreas();
            BundleTable.Bundles.RegisterTemplateBundles();
            RegisterGlobalFilters(GlobalFilters.Filters);

        }

        void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            throw new System.NotImplementedException();
        }
    }
}