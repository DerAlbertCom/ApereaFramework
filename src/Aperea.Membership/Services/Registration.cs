using System.Linq;
using Aperea.EntityModels;
using Aperea.Repositories;

namespace Aperea.Services
{
    public class Registration : IRegistration
    {
        public const string ConfirmWebUserAction = "ConfirmWebUser";
        public const string PasswordResetAction = "PasswordReset";

        private readonly IRepository<Login> _repository;
        private readonly IUserRegistrationMail _mail;
        private readonly IHashing _hashing;
        private readonly IRemoteActionChamber _remoteActionChamber;

        public Registration(IRepository<Login> repository,
                            IUserRegistrationMail mail,
                            IHashing hashing,
                            IRemoteActionChamber remoteActionChamber)
        {
            _repository = repository;
            _remoteActionChamber = remoteActionChamber;
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

            if (login != null && login.Confirmed)
            {
                return UserRegistrationResult.Exists;
            }

            RemoteAction remoteAction;

            if (login != null)
            {
                remoteAction = _remoteActionChamber.GetActiveAction(ConfirmWebUserAction, username);
                _mail.SendRegistrationConfirmation(login, remoteAction);
            }
            else
            {
                if (!UserDataIsValid(username, email))
                {
                    return UserRegistrationResult.InvalidUserdata;
                }
                login = new Login(username, email);
                login.SetPassword(password, _hashing);
                _repository.Add(login);
                _repository.SaveAllChanges();
                remoteAction = _remoteActionChamber.CreateAction(ConfirmWebUserAction, username);
                _mail.SendRegistrationConfirmation(login, remoteAction);
            }
            return UserRegistrationResult.Ok;
        }

        private string Normalize(string text)
        {
            text = text.ToLowerInvariant();
            while (text.Contains("  "))
            {
                text = text.Replace("  ", " ");
            }
            return text;
        }

        private bool UserDataIsValid(string username, string email)
        {
            return _repository.Entities.Where(u => u.Username == username || u.EMail == email).Count() == 0;
        }


        private Login GetExistingUser(string username, string email)
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
            _remoteActionChamber.RemoveAction(ConfirmWebUserAction, username);
            return UserConfirmationResult.Confirmed;
        }

        public void StartPasswordReset(string email)
        {
            var webUser = _repository.Entities.Where(e => e.EMail == email).SingleOrDefault();
            if (webUser == null)
                return;
            if (!webUser.Confirmed)
            {
                var webAction = _remoteActionChamber.GetActiveAction(ConfirmWebUserAction, webUser.Username);
                _mail.SendRegistrationConfirmation(webUser, webAction);
            }
            else
            {
                var webAction = _remoteActionChamber.CreateAction(PasswordResetAction, email);
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

            var webAction = _remoteActionChamber.GetActiveAction(PasswordResetAction, webUser.EMail);
            if (webAction == null)
                return ChangePasswordResult.Error;

            webUser.SetPassword(newPassword, _hashing);
            _repository.SaveAllChanges();
            _remoteActionChamber.RemoveAction(PasswordResetAction, webUser.EMail);
            return ChangePasswordResult.Ok;
        }
    }
}