using System;
using System.Configuration;
using System.Globalization;

namespace Aperea.Settings
{
    public class ApplicationSettings : IApplicationSettings
    {
        private readonly IJsonSettings settings;

        public ApplicationSettings():this(new JsonSettings())
        {
            
        }
        public ApplicationSettings(IJsonSettings settings)
        {
            this.settings = settings;
        }

        public T Get<T>(string key)
        {
            return Get(key, () => default(T));
        }

        public T Get<T>(string key, Func<T> defaultValueFunction)
        {
            string value = ConfigurationManager.AppSettings[key];
            if (string.IsNullOrEmpty(value))
            {
                return settings.Get(key, defaultValueFunction);
            }
            return ChangeType<T>(value);
        }

        public Connection GetConnectionString(string name)
        {
            var connectionString = ConfigurationManager.ConnectionStrings[name];
            if (connectionString == null)
            {
                return settings.GetConnectionString(name);
            }
            return new Connection(connectionString.ConnectionString, connectionString.ProviderName);
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