using System.Web.Mvc;
using Aperea.MVC.Controllers;
using Aperea.MVC.Membership.Areas.Model;
using Aperea.MVC.Security;
using Aperea.Services;

namespace Aperea.MVC.Membership.Areas.Controllers
{
    public class AccountController : ApereaBaseController
    {
        readonly ILoginValidation _validation;

        public AccountController(ILoginValidation validation)
        {
            _validation = validation;
        }

        //
        // GET: /Account/LogOn

        public ActionResult LogOn()
        {
            return View(new LogOnModel());
        }

        //
        // POST: /Account/LogOn

        [HttpPost]
        public ActionResult LogOn(LogOnModel model, string returnUrl)
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
                    else
                    {
                        return RedirectToHomepage();
                    }
                }
                else
                {
                    ModelState.AddModelError("", "The user name or password provided is incorrect.");
                }
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