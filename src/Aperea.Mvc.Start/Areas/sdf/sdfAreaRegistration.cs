using System.Web.Mvc;

namespace ApereaStart.Areas.sdf
{
    public class sdfAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "sdf";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "sdf_default",
                "sdf/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
