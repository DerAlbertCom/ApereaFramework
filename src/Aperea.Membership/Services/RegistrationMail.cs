using Aperea.EntityModels;
using Aperea.Services.Models;
using Aperea.UrlBuilder;

namespace Aperea.Services
{
    public class RegistrationMail : IRegistrationMail
    {
        const string RegistrationConfirmationRequest = "Registration_Confirmation";
        const string PasswordResetRequest = "Registration_PasswordResetRequest";

        readonly ISendTemplatedMail _templateMail;
        readonly IRemoteActionUrlBuilder _urlBuilder;

        public RegistrationMail(ISendTemplatedMail templateMail, IRemoteActionUrlBuilder urlBuilder)
        {
            _templateMail = templateMail;
            _urlBuilder = urlBuilder;
        }

        public void SendRegistrationConfirmationRequest(Login login, RemoteAction remoteAction)
        {
            var model = new RegistrationMailModel
                        {
                            ActionUrl = _urlBuilder.GetUrl(remoteAction),
                            Login = login.Loginname,
                        };
            _templateMail.SendMail(login.EMail, RegistrationConfirmationRequest, model);
        }

        public void SendPasswordResetRequest(Login login, RemoteAction remoteAction)
        {
            var model = new RegistrationMailModel
                        {
                            ActionUrl = _urlBuilder.GetUrl(remoteAction),
                            Login = login.Loginname,
                        };
            _templateMail.SendMail(login.EMail, PasswordResetRequest, model);
        }
    }
}