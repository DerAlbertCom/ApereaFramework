using System.Linq;
using Aperea.EntityModels;
using Aperea.Repositories;

namespace Aperea.Services
{
    public class Registration : IRegistration
    {
        public const string ConfirmWebUserAction = "ConfirmWebUser";
        public const string PasswordResetAction = "PasswordReset";

        readonly IRepository<Login> _repository;
        readonly IUserRegistrationMail _mail;
        readonly IHashing _hashing;
        readonly IWebActionChamber _webActionChamber;

        public Registration(IRepository<Login> repository,
                                IUserRegistrationMail mail,
                                IHashing hashing,
                                IWebActionChamber webActionChamber)
        {
            _repository = repository;
            _webActionChamber = webActionChamber;
            _hashing = hashing;
            _mail = mail;
        }

        public UserRegistrationResult RegisterNewUser(string username, string email, string password,
                                                      string confirmPassword)
        {
            if (password != confirmPassword)
                return UserRegistrationResult.PasswordMismatch;
            username = Normalize(username);
            email = Normalize(email);
            Login login = GetExistingUser(username, email);

            if (login != null && login.Confirmed) {
                return UserRegistrationResult.Exists;
            }

            RemoteAction remoteAction;

            if (login != null) {
                remoteAction = _webActionChamber.GetActiveAction(ConfirmWebUserAction, username);
                _mail.SendRegistrationConfirmation(login, remoteAction);
            }
            else {
                if (!UserDataIsValid(username, email)) {
                    return UserRegistrationResult.InvalidUserdata;
                }
                login = new Login(username, email);
                login.SetPassword(password, _hashing);
                _repository.Add(login);
                _repository.SaveAllChanges();
                remoteAction = _webActionChamber.CreateAction(ConfirmWebUserAction, username);
                _mail.SendRegistrationConfirmation(login, remoteAction);
            }
            return UserRegistrationResult.Ok;
        }

        string Normalize(string text)
        {
            text = text.ToLowerInvariant();
            while (text.Contains("  ")) {
                text = text.Replace("  ", " ");
            }
            return text;
        }

        bool UserDataIsValid(string username, string email)
        {
            return _repository.Entities.Where(u => u.Username == username || u.EMail == email).Count() == 0;
        }


        Login GetExistingUser(string username, string email)
        {
            return _repository.Entities.Where(e => e.Username == username && e.EMail == email).FirstOrDefault();
        }

        public UserConfirmationResult ConfirmUser(string username)
        {
            Login login = _repository.Entities.Where(e => e.Username == username).Single();
            if (login.Confirmed)
                return UserConfirmationResult.Error;
            login.Confirm();
            _repository.SaveAllChanges();
            _webActionChamber.RemoveAction(ConfirmWebUserAction, username);
            return UserConfirmationResult.Confirmed;
        }

        public void StartPasswordReset(string email)
        {
            var webUser = _repository.Entities.Where(e => e.EMail == email).SingleOrDefault();
            if (webUser == null)
                return;
            if (!webUser.Confirmed) {
                var webAction = _webActionChamber.GetActiveAction(ConfirmWebUserAction, webUser.Username);
                _mail.SendRegistrationConfirmation(webUser, webAction);
            }
            else {
                var webAction = _webActionChamber.CreateAction(PasswordResetAction, email);
                _mail.SendPasswordResetRequest(webUser, webAction);
            }
        }

        public ChangePasswordResult ChangePassword(string username, string oldPassword, string newPassword,
                                                   string confirmPassword)
        {
            if (newPassword != confirmPassword)
                return ChangePasswordResult.PasswordMismatch;
            var webUser = _repository.Entities.Where(u => u.Username == username && u.Confirmed).SingleOrDefault();

            if (webUser == null)
                return ChangePasswordResult.Error;

            if (!webUser.IsPasswordValid(oldPassword, _hashing))
                return ChangePasswordResult.InvalidPassword;

            webUser.SetPassword(newPassword, _hashing);
            _repository.SaveAllChanges();

            return ChangePasswordResult.Ok;
        }

        public ChangePasswordResult SetPassword(string username, string newPassword,
                                                string confirmPassword)
        {
            if (newPassword != confirmPassword)
                return ChangePasswordResult.PasswordMismatch;

            var webUser = _repository.Entities.Where(u => u.Username == username && u.Confirmed).SingleOrDefault();
            if (webUser == null)
                return ChangePasswordResult.Error;

            var webAction = _webActionChamber.GetActiveAction(PasswordResetAction, webUser.EMail);
            if (webAction == null)
                return ChangePasswordResult.Error;

            webUser.SetPassword(newPassword, _hashing);
            _repository.SaveAllChanges();
            _webActionChamber.RemoveAction(PasswordResetAction, webUser.EMail);
            return ChangePasswordResult.Ok;
        }
    }
}