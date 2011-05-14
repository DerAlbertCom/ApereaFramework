using System.Linq;
using Aperea.Context;
using Aperea.EntityModels;
using Aperea.Repositories;
using RazorEngine;

namespace Aperea.Services
{
    public class SendTemplatedMail : ISendTemplatedMail
    {
        private readonly IRepository<MailTemplate> _repository;
        private readonly ICultureContext _context;
        private readonly ISendMail _sendMail;

        public SendTemplatedMail(IRepository<MailTemplate> repository, ISendMail sendMail, ICultureContext context)
        {
            _repository = repository;
            _context = context;
            _sendMail = sendMail;
        }

        public void SendMail<T>(string recipient, string templateName, T model)
        {
            var mailTemplate = GetMailTemplate(templateName);
            var subject = Razor.Parse(mailTemplate.Subject, model);
            var body = Razor.Parse(mailTemplate.Body, model);
            _sendMail.Send(recipient, subject, body);
        }

        private MailTemplate GetMailTemplate(string templateName)
        {
            var culture = _context.CurrentCulture;
            var mailTemplate = _repository.Entities
                .Where(mt => mt.SystemLanguage.Culture == culture && mt.TemplateName == templateName)
                .SingleOrDefault();
            if (mailTemplate != null)
                return mailTemplate;
            return GetFirstOrMissingTemplate(templateName, culture);
        }

        private MailTemplate GetFirstOrMissingTemplate(string templateName, string culture)
        {
            var mailTemplate = _repository.Entities
                .Where(mt => mt.SystemLanguage.Culture == culture && mt.TemplateName == templateName)
                .FirstOrDefault();
            if (mailTemplate != null)
                return mailTemplate;
            return new MailTemplate
                       {
                           Subject = string.Format(MailStrings.Information_MissingTemplate, templateName, culture),
                           Body =
                               string.Format(MailStrings.Information_MissingTemplateForModel,
                                             templateName,
                                             culture),
                           TemplateName = templateName
                       };
        }
    }
}