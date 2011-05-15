using System.Web.Mvc;
using Aperea.EntityModels;

namespace Aperea.MVC.RemoteActions
{
    public interface IRemoteActionWorker
    {
        ActionResult Execute(RemoteAction webAction, ControllerContext context);
    }
}