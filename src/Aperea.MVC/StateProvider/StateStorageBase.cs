using System;
using System.Web;

namespace Aperea.MVC.StateProvider
{
    public abstract class StateStorageBase : IStateStorage
    {
        private readonly Func<string, object> _getAccessor;
        private readonly Action<string, object> _setAccessor;

        protected StateStorageBase(Func<string, object> getAccessor, Action<string, object> setAccessor)
        {
            _getAccessor = getAccessor;
            _setAccessor = setAccessor;
        }

        public T Get<T>(string key, Func<T> defaultStateCreator)
        {
            var value = _getAccessor(key);
            if (value == null)
            {
                value = defaultStateCreator();
                _setAccessor(key, value);
            }
            return (T) value;
        }

        public T Get<T>(string key)
        {
            return Get(key, () => default(T));
        }

        public void Set<T>(string key, T value)
        {
            _setAccessor(key, value);
        }

        protected static HttpContextBase Context
        {
            get { return new HttpContextWrapper(HttpContext.Current); }
        }
    }
}