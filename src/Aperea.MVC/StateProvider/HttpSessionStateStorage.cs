namespace Aperea.MVC.StateProvider
{
    public class HttpSessionStateStorage : StateStorageBase
    {
        public HttpSessionStateStorage()
            : base(key => Context.Session[key], (key, value) => Context.Session[key] = value)
        {
        }
    }
}