using System;

namespace Aperea.Settings
{
    public interface IApplicationSettings
    {
        T Get<T>(string key);
        T Get<T>(string key, Func<T> defaultValueFunction);

        Connection GetConnectionString(string name);
    }
}