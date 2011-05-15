namespace Aperea.Services
{
    public interface IRegistration
    {
        RegistrationResult RegisterNewLogin(string loginname, string email, string password,
                                               string confirmPassword);

        RegistrationConfirmationResult ConfirmLogin(string loginname);

        void StartPasswordReset(string email);

        ChangePasswordResult ChangePassword(string loginname, string oldPassword, string newPassword,
                                            string confirmPassword);

        ChangePasswordResult SetPassword(string loginname, string newPassword,
                                         string confirmPassword);
    }
}