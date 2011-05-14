namespace Aperea.Services
{
    public interface IUserRegistration
    {
        UserRegistrationResult RegisterNewUser(string username, string email, string password,
                                               string confirmPassword);

        UserConfirmationResult ConfirmUser(string username);

        void StartPasswordReset(string email);

        ChangePasswordResult ChangePassword(string username, string oldPassword, string newPassword,
                                            string confirmPassword);

        ChangePasswordResult SetPassword(string username, string newPassword,
                                    string confirmPassword);

    }
}