using System.Web.Mvc;
using Aperea.EntityModels;
using Aperea.MVC.Areas.Membership.Controllers;
using Aperea.MVC.Areas.Membership.Models;
using Aperea.MVC.Attributes;
using Aperea.MVC.Extensions;
using Aperea.MVC.RemoteActions;
using Aperea.Services;

namespace Aperea.MVC.Areas.Membership.RemoteActions
{
    [RemoteActionName(Registration.ConfirmLoginAction)]
    public class ConfirmNewLoginActionWorker : IRemoteActionWorker
    {
        readonly IRegistration _registration;

        public ConfirmNewLoginActionWorker(IRegistration registration)
        {
            _registration = registration;
        }

        public ActionResult Execute(RemoteAction webAction, ControllerContext context)
        {
            if (_registration.ConfirmLogin(webAction.Parameter) == RegistrationConfirmationResult.Confirmed)
            {
                var model = new RegisterLoginViewModel {LoginName = webAction.Parameter};
                context.Controller.TempData.SetModel(model);
                return RouteHelper.RedirectTo<RegistrationController>(c => c.RegisterLoginConfirmed());
            }
            return new HttpNotFoundResult();
        }
    }
}