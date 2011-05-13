using System.Web.Mvc;
using Aperea.Infrastructure.Bootstrap;

namespace Aperea.MVC.Initialize
{
    public class InfrastructureMvcFiltersInitialize : IBootstrapItem
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