using System.Web.Mvc;

namespace ApereaStart.Controllers
{
    public class HomeController : ApereaStartBaseController
    {
         public ActionResult Index()
         {
             return new ViewResult();
         }
    }
}