using Aperea.EntityModels;

namespace Aperea.Services
{
    public interface IUserRegistrationMail
    {
        void SendRegistrationConfirmation(Login login, RemoteAction remoteAction);
        void SendPasswordResetRequest(Login login, RemoteAction remoteAction);
    }
}