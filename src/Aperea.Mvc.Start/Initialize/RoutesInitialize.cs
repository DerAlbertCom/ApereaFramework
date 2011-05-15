using System.Web.Mvc;
using System.Web.Routing;
using Aperea.Infrastructure.Bootstrap;
using Aperea.MVC.Settings;

namespace ApereaStart.Initialize
{
    public class RoutesInitialize : IBootstrapItem
    {
        private readonly ICultureSettings _cultureSettings;

        public RoutesInitialize(ICultureSettings cultureSettings)
        {
            _cultureSettings = cultureSettings;
        }

        public void Execute()
        {
            AreaRegistration.RegisterAllAreas();

            Register(RouteTable.Routes);
        }

        void Register(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Account", // Route name
                "Account/{action}/{id}", // URL with parameters
                new {
                    culture = _cultureSettings,
                    controller = "Account",
                    action = "Index",
                    id = UrlParameter.Optional
                } // Parameter defaults
                );

            routes.MapRoute(
                "HashKey", // Route name
                "{culture}/{controller}/{action}/{hash}/{key}", // URL with parameters
                new {
                    culture = "",
                } // Parameter defaults
                );

            routes.MapRoute(
                "Default", // Route name
                "{culture}/{controller}/{action}/{id}", // URL with parameters
                new {
                    culture = _cultureSettings,
                    controller = "Home",
                    action = "Index",
                    id = UrlParameter.Optional
                } // Parameter defaults
                );
        }
    }
}