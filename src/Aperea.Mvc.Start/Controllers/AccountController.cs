using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Aperea.MVC.Controllers;
using Aperea.Services;
using ApereaStart.Models;

namespace ApereaStart.Controllers
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
                    SetAuthenticationTicket(model);

                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                        && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
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

         void SetAuthenticationTicket(LogOnModel model)
        {
             FormsAuthentication.SetAuthCookie(model.UserName,model.RememberMe);
        }

        //
        // GET: /Account/LogOff

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }
    }
}