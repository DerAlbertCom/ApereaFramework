using System;
using System.Web.Mvc;
using Aperea.MVC.RemoteActions;

namespace ApereaStart.Controllers
{
    public class RemoteActionController : ApereaStartBaseController
    {
        private readonly IRemoteActionWorkerFactory _factory;

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