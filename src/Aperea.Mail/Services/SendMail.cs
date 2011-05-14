using System.Net.Mail;
using Aperea.Settings;

namespace Aperea.Services
{
    public class SendMail : ISendMail
    {
        private readonly IMailSettings _settings;

        public SendMail(IMailSettings settings)
        {
            _settings = settings;
        }

        public void Send(string recipient, string subject, string body)
        {
            var message = new MailMessage(new MailAddress(_settings.MailSender), new MailAddress(recipient))
                              {
                                  Subject = subject,
                                  Body = body
                              };
            var smtp = new SmtpClient();
            smtp.Send(message);
        }
    }
}