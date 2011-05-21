using System.Web.Mvc;
using Aperea.EntityModels;
using Aperea.MVC.Attributes;
using Aperea.MVC.Extensions;
using Aperea.MVC.RemoteActions;
using Aperea.Services;
using ApereaStart.Controllers;
using ApereaStart.Models;

namespace ApereaStart.RemoteActions
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