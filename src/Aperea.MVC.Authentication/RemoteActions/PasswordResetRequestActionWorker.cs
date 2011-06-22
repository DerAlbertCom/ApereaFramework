using System;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Routing;
using Aperea.EntityModels;
using Aperea.MVC.Attributes;
using Aperea.MVC.Authentication.Controllers;
using Aperea.MVC.Authentication.Models;
using Aperea.MVC.Extensions;
using Aperea.MVC.RemoteActions;
using Aperea.Repositories;
using Aperea.Services;

namespace Aperea.MVC.Authentication.RemoteActions
{
    [RemoteActionName(Registration.PasswordResetAction)]
    public class PasswordResetRequestActionWorker : IRemoteActionWorker
    {
        readonly IRegistration _userRegistration;
        readonly IRepository<Login> _repository;

        public PasswordResetRequestActionWorker(IRegistration userRegistration, IRepository<Login> repository)
        {
            _userRegistration = userRegistration;
            _repository = repository;
        }

        public ActionResult Execute(RemoteAction webAction, ControllerContext context)
        {
            Login webUser = TryGetWebUser(webAction.Parameter);
            if (webUser != null)
            {
                var model = new PasswordResetViewModel {Username = webUser.Loginname};
                context.Controller.TempData.SetModel(model);
                return RouteHelper.RedirectTo<RegistrationController>(c => c.PasswordReset());
            }
            return new HttpNotFoundResult();
        }

        static RouteValueDictionary GetValues<T>(Expression<Action<T>> expression) where T : Controller
        {
            return Microsoft.Web.Mvc.Internal.ExpressionHelper.GetRouteValuesFromExpression(expression);
        }

        Login TryGetWebUser(string email)
        {
            return _repository.Entities.Where(u => u.EMail == email && u.Confirmed).SingleOrDefault();
        }
    }
}