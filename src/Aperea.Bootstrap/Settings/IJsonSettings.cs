using System;

namespace Aperea.Settings
{
    public interface IJsonSettings
    {        
        T Get<T>(string key, Func<T> defaultValueFunction);
        Connection GetConnectionString(string key);
    }
}