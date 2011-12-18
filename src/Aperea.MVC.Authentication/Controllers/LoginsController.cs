using System.Web.Mvc;

namespace Aperea.MVC.Authentication.Controllers
{
    public class LoginsController : AdministratorsController
    {
        public ActionResult All()
        {
            return View();
        }
    }
}