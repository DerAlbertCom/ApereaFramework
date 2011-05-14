using Aperea.EntityModels;
using Aperea.Services.Models;
using Aperea.UrlBuilder;

namespace Aperea.Services
{
    public class UserRegistrationMail : IUserRegistrationMail
    {
        private const string UserRegistrationConfirmation = "UserRegistration_Confirmation";
        private const string PasswordResetRequest = "UserRegistration_PasswordResetRequest";

        private readonly ISendTemplatedMail _templateMail;
        private readonly IRemoteActionUrlBuilder _urlBuilder;

        public UserRegistrationMail(ISendTemplatedMail templateMail, IRemoteActionUrlBuilder urlBuilder)
        {
            _templateMail = templateMail;
            _urlBuilder = urlBuilder;
        }

        public void SendRegistrationConfirmation(Login login, RemoteAction remoteAction)
        {
            var model = new UserRegistrationMailModel
                            {
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