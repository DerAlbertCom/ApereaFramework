using System.Web.Mvc;
using Aperea.MVC.Controllers;

namespace ApereaStart.Controllers
{
    public class HomeController : ApereaBaseController
    {
        public ActionResult Index()
        {
            return new ViewResult();
        }
    }
}