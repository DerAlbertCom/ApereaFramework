using Aperea.EntityModels;

namespace Aperea.MVC.RemoteActions
{
    public interface IRemoteActionHashing
    {
        string GetHash(RemoteAction action);
    }
}