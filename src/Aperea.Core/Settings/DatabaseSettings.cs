namespace Aperea.Settings
{
    public class DatabaseSettings : IDatabaseSettings
    {
        readonly IApplicationSettings _settings;

        public DatabaseSettings(IApplicationSettings settings)
        {
            _settings = settings;
        }

        public string DatabaseName
        {
            get { return _settings.Get("Raven.DatabaseName", () => "ApereaFramework"); }
        }

        public string ConnectionStringName
        {
            get { return _settings.Get("Raven.ConnectionStringName", () => "RavenDB"); }
        }
    }
}