using System.Web.Mvc;
using Aperea.Infrastructure.Bootstrap;

namespace ApereaStart.Initialize
{
    public class GlobalFiltersInitialize : IBootstrapItem
    {
        public void Execute()
        {
            RegisterGlobalFilters(GlobalFilters.Filters);
        }

        private void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}