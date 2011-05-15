using Aperea.EntityModels;

namespace Aperea.Services
{
    public interface IRemoteActionChamber
    {
        RemoteAction CreateAction(string actionName, string parameter);
        RemoteAction GetActiveAction(string actionName, string parameter);
        void RemoveAction(string actionName, string parameter);
    }
}