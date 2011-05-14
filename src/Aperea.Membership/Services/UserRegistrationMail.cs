using Aperea.EntityModels;
using Aperea.Services.Models;
using Aperea.UrlBuilder;

namespace Aperea.Services
{
    public class UserRegistrationMail : IUserRegistrationMail
    {
        const string UserRegistrationConfirmation = "UserRegistration_Confirmation";
        const string PasswordResetRequest = "UserRegistration_PasswordResetRequest";

        readonly ISendTemplatedMail _templateMail;
        readonly IRemoteActionUrlBuilder _urlBuilder;

        public UserRegistrationMail(ISendTemplatedMail templateMail, IRemoteActionUrlBuilder urlBuilder)
        {
            _templateMail = templateMail;
            _urlBuilder = urlBuilder;
        }

        public void SendRegistrationConfirmation(Login login, RemoteAction remoteAction)
        {
            var model = new UserRegistrationMailModel {
                ActionUrl = _urlBuilder.GetUrl(remoteAction),
                Username = login.Username,
            };
            _templateMail.SendMail(login.EMail, UserRegistrationConfirmation, model);
        }

        public void SendPasswordResetRequest(Login login, RemoteAction remoteAction)
        {
            var model = new UserRegistrationMailModel
            {
                ActionUrl = _urlBuilder.GetUrl(remoteAction),
                Username = login.Username,
            };
            _templateMail.SendMail(login.EMail, PasswordResetRequest, model);
        }
    }
}