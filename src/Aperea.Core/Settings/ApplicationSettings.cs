using System;
using System.Configuration;
using System.Globalization;

namespace Aperea.Settings
{
    public class ApplicationSettings : IApplicationSettings
    {
        public T Get<T>(string key)
        {
            return Get<T>(key, () => default(T));
        }

        public T Get<T>(string key, Func<T> defaultFunc)
        {
            string value = ConfigurationManager.AppSettings[key];
            if (string.IsNullOrEmpty(value))
                return defaultFunc();
            return (T) Convert.ChangeType(value, typeof (T), CultureInfo.InvariantCulture);
        }
    }
}