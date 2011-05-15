using System;
using System.Web.Mvc;

namespace Aperea.MVC.RemoteActions
{
    public interface IRemoteActionWorkerFactory
    {
        ActionResult ExecuteWebAction(string hash, Guid guid, ControllerContext controllerContext);
    }
}