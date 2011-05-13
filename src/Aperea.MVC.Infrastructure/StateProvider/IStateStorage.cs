using System;

namespace Aperea.MVC.StateProvider
{
    public interface IStateStorage
    {
        T Get<T>(string key, Func<T> defaultStateCreator);
        T Get<T>(string key);
        void Set<T>(string key, T value);
    }
}