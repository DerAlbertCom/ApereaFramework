namespace Aperea.Settings
{
    public class MailSettings : IMailSettings
    {
        private readonly IApplicationSettings _settings;

        public MailSettings(IApplicationSettings settings)
        {
            _settings = settings;
        }

        public string MailSender
        {
            get { return _settings.Get<string>("Mail.Sender"); }
        }
    }
}