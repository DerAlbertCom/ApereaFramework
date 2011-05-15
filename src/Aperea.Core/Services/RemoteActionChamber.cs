using System;
using System.Linq;
using Aperea.EntityModels;
using Aperea.Repositories;

namespace Aperea.Services
{
    public class RemoteActionChamber : IRemoteActionChamber
    {
        private readonly IRepository<RemoteAction> _repository;

        public RemoteActionChamber(IRepository<RemoteAction> repository)
        {
            _repository = repository;
        }

        public RemoteAction CreateAction(string actionName, string parameter)
        {
            RemoteAction action = TryGetActiveAction(actionName, parameter);
            if (action == null)
            {
                action = new RemoteAction(actionName, parameter);
                _repository.Add(action);
            }
            else
            {
                action.ConfirmationKey = Guid.NewGuid();
            }

            _repository.SaveAllChanges();
            return action;
        }

        private RemoteAction TryGetActiveAction(string actionName, string parameter)
        {
            return _repository.Entities.Where(e => e.Action == actionName && e.Parameter == parameter).SingleOrDefault();
        }

        public RemoteAction GetActiveAction(string actionName, string parameter)
        {
            return _repository.Entities.Where(e => e.Action == actionName && e.Parameter == parameter).Single();
        }

        public void RemoveAction(string actionName, string parameter)
        {
            var action = _repository.Entities.Where(e => e.Action == actionName && e.Parameter == parameter).Single();
            _repository.Remove(action);
            _repository.SaveAllChanges();
        }
    }
}