using System;
using System.Linq;
using System.Web.Mvc;
using Aperea.EntityModels;
using Aperea.Repositories;
using Microsoft.Practices.ServiceLocation;

namespace Aperea.MVC.RemoteActions
{
    public class RemoteActionWorkerFactory : IRemoteActionWorkerFactory
    {
        readonly IRepository<RemoteAction> _repository;
        readonly IRemoteActionHashing _hashing;

        public RemoteActionWorkerFactory(IRepository<RemoteAction> repository, IRemoteActionHashing hashing)
        {
            _repository = repository;
            _hashing = hashing;
        }

        public ActionResult ExecuteWebAction(string hash, Guid guid, ControllerContext controllerContext)
        {
            var webAction = FindWebAction(hash, guid);
            if (webAction == null)
                return new HttpNotFoundResult();
            var worker = ServiceLocator.Current.GetInstance<IRemoteActionWorker>(webAction.Action);
            return worker.Execute(webAction, controllerContext);
        }

        RemoteAction FindWebAction(string hash, Guid guid)
        {
            var remoteActions = from wa in _repository.Entities where wa.ConfirmationKey == guid select wa;
            return remoteActions.SingleOrDefault(ra => _hashing.GetHash(ra) == hash);
        }
    }
}