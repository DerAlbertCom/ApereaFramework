using Aperea.EntityModels;

namespace Aperea.Services
{
    public interface IRegistrationMail
    {
        void SendRegistrationConfirmationRequest(Login login, RemoteAction remoteAction);
        void SendPasswordResetRequest(Login login, RemoteAction remoteAction);
    }
}