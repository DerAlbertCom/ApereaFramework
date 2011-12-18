using System.Web.Mvc;
using Aperea.MVC.Authentication.Models;
using Aperea.MVC.Controllers;
using Aperea.MVC.Security;
using Aperea.Services;

namespace Aperea.MVC.Authentication.Controllers
{
    public class LogonController : ApereaBaseController
    {
        readonly ILoginValidation _validation;

        public LogonController(ILoginValidation validation)
        {
            _validation = validation;
        }

        //
        // GET: /Account/LogOn

        public ActionResult Index()
        {
            return View(new LogOnModel());
        }

        //
        // POST: /Account/LogOn

        [HttpPost]
        public ActionResult Index(LogOnModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (_validation.ValidateLoginForLogon(model.UserName, model.Password))
                {
                    ApereaFormsAuthentication.SignOn(model.UserName, model.RememberMe);

                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                        && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                    {
                        return Redirect(returnUrl);
                    }
                    return RedirectToHomepage();
                }
                ModelState.AddModelError("", MvcResourceStrings.Error_UserName_Or_Password_is_not_correct);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        // GET: /Account/LogOff

        public ActionResult LogOff()
        {
            ApereaFormsAuthentication.SignOut();

            return RedirectToHomepage();
        }
    }
}