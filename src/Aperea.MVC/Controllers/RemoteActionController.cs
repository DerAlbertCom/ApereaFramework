using System;
using System.Web.Mvc;
using Aperea.MVC.RemoteActions;

namespace Aperea.MVC.Controllers
{
    public class RemoteActionController : ApereaBaseController
    {
        readonly IRemoteActionWorkerFactory _factory;

        public RemoteActionController(IRemoteActionWorkerFactory factory)
        {
            _factory = factory;
        }

        public ActionResult Validate(string hash, Guid key)
        {
            return _factory.ExecuteWebAction(hash, key, ControllerContext);
        }
    }
}