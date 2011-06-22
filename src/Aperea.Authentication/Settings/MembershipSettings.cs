namespace Aperea.Settings
{
    public class MembershipSettings : IMembershipSettings
    {
        readonly IApplicationSettings _settings;

        public MembershipSettings(IApplicationSettings settings)
        {
            _settings = settings;
        }

        public string AdministratorLogin
        {
            get { return _settings.Get("Administrator.Login", () => "admin"); }
        }

        public string AdministratorEMail
        {
            get { return _settings.Get("Administrator.EMail", () => "admin@mail.local"); }
        }

        public string AdministratorPassword
        {
            get { return _settings.Get("Administrator.Password", () => "password"); }
        }
    }
}