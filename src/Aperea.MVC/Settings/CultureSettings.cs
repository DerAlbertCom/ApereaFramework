using Aperea.Settings;

namespace Aperea.MVC.Settings
{
    public class CultureSettings : ICultureSettings
    {
        readonly IApplicationSettings _settings;

        public CultureSettings(IApplicationSettings settings)
        {
            _settings = settings;
        }

        public string[] PossibleCultures
        {
            get { return _settings.Get<string>("Culture.PossibleCultures").Split(new[] {';', ','}); }
        }

        public string DefaultCulture
        {
            get { return _settings.Get("Culture.Default", () => "en"); }
        }
    }
}