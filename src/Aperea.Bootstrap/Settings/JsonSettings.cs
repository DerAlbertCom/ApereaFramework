using System;
using System.Configuration;
using Newtonsoft.Json;

namespace Aperea.Settings
{
    public class JsonSettings : IJsonSettings
    {
        private readonly dynamic json;

        public JsonSettings() : this(new SettingJsonReader())
        {
        }

        public JsonSettings(ISettingJsonReader reader)
        {
            json = JsonConvert.DeserializeObject<dynamic>(reader.Load());
        }


        public T Get<T>(string key, Func<T> defaultValueFunction)
        {
            if (json == null)
            {
                return defaultValueFunction();
            }
            dynamic appSettings = json["appSettings"];
            if (appSettings == null)
            {
                return defaultValueFunction();
            }
            var value = appSettings[key];
            if (value == null)
            {
                return defaultValueFunction();
            }
            return Convert.ChangeType(value, typeof (T));
        }

        public Connection GetConnectionString(string key)
        {
            dynamic connectionStrings = json["connectionStrings"];
            if (connectionStrings == null)
            {
                throw new ConfigurationErrorsException("missing connectionStrings in Settings.json");
            }
            var value = connectionStrings[key];
            if (value == null)
            {
                throw new ConfigurationErrorsException("missing " + key + " in connectionStrings of Settings.json");
            }
            var connectionString = value.Value as string;
            if (connectionString != null)
            {
                return new Connection(connectionString);
            }
            return new Connection(value.connectionString.Value as string, value.providerName.Value as string ?? "");
        }
    }
}