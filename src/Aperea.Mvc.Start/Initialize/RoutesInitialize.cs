using System.Web.Mvc;
using System.Web.Routing;
using Aperea.Infrastructure.Bootstrap;
using Aperea.Settings;

namespace ApereaStart.Initialize
{
    public class RoutesInitialize : IBootstrapItem
    {
        readonly ICultureSettings _cultureSettings;

        public RoutesInitialize(ICultureSettings cultureSettings)
        {
            _cultureSettings = cultureSettings;
        }

        public void Execute()
        {
            AreaRegistration.RegisterAllAreas();

            Register(RouteTable.Routes);
        }

        public int Order
        {
            get { return 0; }
        }

        void Register(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Logon", // Route name
                "Logon/{action}/{id}", // URL with parameters
                new
                {
                    area="account",
                    culture = "",
                    controller = "Logon",
                    action = "Index",
                    id = UrlParameter.Optional
                } // Parameter defaults
                );

            routes.MapRoute(
                "Default", // Route name
                "{culture}/{controller}/{action}/{id}", // URL with parameters
                new
                {
                    culture = _cultureSettings.DefaultCulture,
                    controller = "Home",
                    action = "Index",
                    id = UrlParameter.Optional
                } // Parameter defaults
                );
        }
    }
}