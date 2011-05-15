namespace Aperea.MVC.StateProvider
{
    public class HttpApplicationContextStateStorage : StateStorageBase
    {
        public HttpApplicationContextStateStorage()
            : base(key => Context.Application[key], (key, value) => Context.Application[key] = value)
        {
        }
    }
}