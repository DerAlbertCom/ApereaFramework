using Aperea.EntityModels;
using Aperea.Services;

namespace Aperea.MVC.RemoteActions
{
    public class RemoteActionHashing : IRemoteActionHashing
    {
        readonly IHashing _hashing;

        public RemoteActionHashing(IHashing hashing)
        {
            _hashing = hashing;
        }

        public string GetHash(RemoteAction action)
        {
            string hash = _hashing.GetHash(action.Action, action.Parameter).Substring(0, 8);
            return hash.Replace("/", "I").Replace("\\", "U").Replace("+", "4");
        }
    }
}