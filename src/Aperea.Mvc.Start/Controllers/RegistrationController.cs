using System.Web.Mvc;
using Aperea.MVC.Extensions;
using Aperea.Services;
using ApereaStart.Models;

namespace ApereaStart.Controllers
{
    public class RegistrationController : ApereaStartBaseController
    {
        readonly IRegistration _userRegistration;
        readonly IHashing _hashing;

        public RegistrationController(IRegistration userRegistration, IHashing hashing)
        {
            _userRegistration = userRegistration;
            _hashing = hashing;
        }

        public ActionResult RegisterLogin()
        {
            TempData.Clear();
            return View(new RegisterLoginViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegisterLogin(RegisterLoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = _userRegistration.RegisterNewLogin(model.LoginName, model.EMail, model.Password,
                                                                model.ConfirmPassword);
                if (result == RegistrationResult.Ok)
                {
                    return RedirectToAction("RegisterLoginOk");
                }
                TempData.SetModel(model);
                ModelState.AddModelError("", result.Text);
            }
            return View(model);
        }

        public ActionResult RegisterLoginOk()
        {
            return View(TempData.GetModel<RegisterLoginViewModel>());
        }

        public ActionResult RegisterLoginConfirmed()
        {
            return View(TempData.GetModel<RegisterLoginViewModel>());
        }

        public ActionResult PasswordResetRequest()
        {
            return View(new PasswordResetRequestViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PasswordResetRequest(PasswordResetRequestViewModel model)
        {
            if (ModelState.IsValid)
            {
                _userRegistration.StartPasswordReset(model.EMail);
                TempData.SetModel(model);
                return RedirectToAction("PasswordResetRequestOk");
            }
            return View(model);
        }

        public ActionResult PasswordResetRequestOk()
        {
            return View(TempData.GetModel<PasswordResetRequestViewModel>());
        }

        public ActionResult PasswordReset()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("ChangePassword");

            var model = TempData.GetModel<PasswordResetViewModel>();
            if (string.IsNullOrEmpty(model.Username))
                return RedirectToAction("Index", "Home");

            SetSecurityToken(model);
            return View(model);
        }

        void SetSecurityToken(PasswordResetViewModel model)
        {
            Session["PasswordResetSecurityToken"] = _hashing.GetHash(Session.SessionID, model.Username);
        }

        void ClearSecurityToken()
        {
            Session.Remove("PasswordResetSecurityToken");
        }

        bool IsSecurityTokenValid(PasswordResetViewModel model)
        {
            return
                string.Compare((string) Session["PasswordRestSecurityToken"],
                               _hashing.GetHash(Session.SessionID, model.Username)) == 0;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PasswordReset(PasswordResetViewModel model)
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("ChangePassword");

            if (!IsSecurityTokenValid(model))
                return RedirectToAction("Index", "Home");
            if (ModelState.IsValid)
            {
                var result = _userRegistration.SetPassword(model.Username, model.Password, model.ConfirmPassword);
                if (result == ChangePasswordResult.Ok)
                {
                    TempData.SetModel(model);
                    return RedirectToAction("PasswordResetOk");
                }
                ModelState.AddModelError("", result.Text);
            }
            return View(model);
        }

        [Authorize]
        public ActionResult ChangePassword()
        {
            return View(new ChangePasswordViewModel());
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(ChangePasswordViewModel model)
        {
            if (string.Compare(model.LoginName, User.Identity.Name, true) != 0)
                return View(model);

            if (ModelState.IsValid)
            {
                var result = _userRegistration.ChangePassword(User.Identity.Name, model.OldPassword, model.Password,
                                                              model.ConfirmPassword);
                if (result == ChangePasswordResult.Ok)
                {
                    TempData.SetModel(model);
                    return RedirectToAction("ChangePasswordOk");
                }
                ModelState.AddModelError("", result.Text);
            }
            return View(model);
        }

        public ActionResult ChangePasswordOk()
        {
            return View(TempData.GetModel<ChangePasswordViewModel>());
        }

        public ActionResult PasswordResetOk()
        {
            ClearSecurityToken();
            return View(TempData.GetModel<PasswordResetViewModel>());
        }
    }
}