namespace Aperea.MVC.StateProvider
{
    public class HttpContextStateStorage : StateStorageBase
    {
        public HttpContextStateStorage()
            : base(key => Context.Items[key], (key, value) => Context.Items[key] = value)
        {
        }
    }
}