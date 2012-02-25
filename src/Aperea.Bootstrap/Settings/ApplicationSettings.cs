using System;
using System.Configuration;
using System.Globalization;

namespace Aperea.Settings
{
    public class ApplicationSettings : IApplicationSettings
    {
        public T Get<T>(string key)
        {
            return Get(key, () => default(T));
        }

        public T Get<T>(string key, Func<T> defaultFunc)
        {
            string value = ConfigurationManager.AppSettings[key];
            if (string.IsNullOrEmpty(value))
                return defaultFunc();
            return ChangeType<T>(value);
        }

        static T ChangeType<T>(string value)
        {
            if (typeof(T).IsEnum)
                return ConvertToEnum<T>(value);
            return (T) Convert.ChangeType(value, typeof (T), CultureInfo.InvariantCulture);
        }

        static T ConvertToEnum<T>(string value)
        {
            return (T) Enum.Parse(typeof (T), value);
        }
    }
}