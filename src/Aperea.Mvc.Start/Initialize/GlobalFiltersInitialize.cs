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

        public int Order
        {
            get { return 0; }
        }

        void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public void Dispose()
        {
        }
    }
}