using System.Web.Mvc;
using Aperea.MVC.ActionFilter;

namespace Aperea.MVC.Controllers
{
    [ThreadCulture]
    [DatabaseContext]
    public abstract class ApereaBaseController : Controller
    {
        protected ActionResult RedirectToHomepage()
        {
            return Redirect("~/");
        }
    }
}