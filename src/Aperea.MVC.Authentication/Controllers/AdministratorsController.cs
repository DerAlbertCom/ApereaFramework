using System;
using System.Web.Mvc;
using Aperea.MVC.Controllers;
using Aperea.Security;

namespace Aperea.MVC.Authentication.Controllers
{
    [Authorize(Roles = AuthenticationGroups.Administrators)]
    public abstract class AdministratorsController : ApereaBaseController
    {
    }
}