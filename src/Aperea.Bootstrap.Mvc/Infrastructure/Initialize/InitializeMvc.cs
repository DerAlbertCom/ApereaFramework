using System;
using System.Web.Mvc;
using Aperea.ActionFilter;
using Aperea.Infrastructure.Bootstrap;

namespace Aperea.Infrastructure.Initialize
{
    public class InitializeMvc : BootstrapItem
    {
        public override void Execute()
        {
            AreaRegistration.RegisterAllAreas();
            RegisterGlobalFilters(GlobalFilters.Filters);
        }

        void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new ValidateAntiForgeryTokenFilter());
            filters.Add(new ReleaseAndDisposeAllHttpScopedObjectsFilter());
        }
    }
}