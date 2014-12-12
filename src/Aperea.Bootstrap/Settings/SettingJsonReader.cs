using System;
using System.IO;
using System.Reflection;

namespace Aperea.Settings
{
    public class SettingJsonReader : ISettingJsonReader
    {
        private const string SettingsJson = "Settings.json";

        public string Load()
        {
            var fileName = GetFileName();
            if (string.IsNullOrWhiteSpace(fileName))
            {
                return "";
            }
            return File.ReadAllText(fileName);
        }

        private string GetFileName()
        {
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, SettingsJson);
            if (File.Exists(path))
                return path;
            path = Path.Combine(GetAssemblyDirectory(GetType().Assembly), SettingsJson);
            if (File.Exists(path))
                return path;

            path = Path.Combine(GetAssemblyDirectory(Assembly.GetExecutingAssembly()), SettingsJson);
            return File.Exists(path) ? path : null;
        }

        private static string GetAssemblyDirectory(Assembly assembly)
        {
            var uri = new UriBuilder(assembly.CodeBase);
            return Path.GetDirectoryName(Uri.UnescapeDataString(uri.Path));
        }
    }
}