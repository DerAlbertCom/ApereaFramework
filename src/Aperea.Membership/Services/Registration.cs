﻿using System.Linq;
using Aperea.EntityModels;
using Aperea.Repositories;

namespace Aperea.Services
{
    public class Registration : IRegistration
    {
        public const string ConfirmLoginAction = "ConfirmLogin";
        public const string PasswordResetAction = "PasswordReset";

        private readonly IRepository<Login> _repository;
        private readonly IRegistrationMail _mail;
        private readonly IHashing _hashing;
        private readonly IRemoteActionChamber _remoteActionChamber;

        public Registration(IRepository<Login> repository,
                            IRegistrationMail mail,
                            IHashing hashing,
                            IRemoteActionChamber remoteActionChamber)
        {
            _repository = repository;
            _remoteActionChamber = remoteActionChamber;
            _hashing = hashing;
            _mail = mail;
        }

        public RegistrationResult RegisterNewLogin(string loginname, string email, string password,
                                                  string confirmPassword)
        {
            if (password != confirmPassword)
                return RegistrationResult.PasswordMismatch;
            loginname = Normalize(loginname);
            email = Normalize(email);
            Login login = GetExistingLogin(loginname, email);

            if (login != null && login.Confirmed)
            {
                return RegistrationResult.Exists;
            }

            RemoteAction remoteAction;

            if (login != null)
            {
                remoteAction = _remoteActionChamber.GetActiveAction(ConfirmLoginAction, loginname);
                _mail.SendRegistrationConfirmationRequest(login, remoteAction);
            }
            else
            {
                if (!LoginDataIsValid(loginname, email))
                {
                    return RegistrationResult.InvalidLoginData;
                }
                login = new Login(loginname, email);
                login.SetPassword(password, _hashing);
                _repository.Add(login);
                _repository.SaveAllChanges();
                remoteAction = _remoteActionChamber.CreateAction(ConfirmLoginAction, loginname);
                _mail.SendRegistrationConfirmationRequest(login, remoteAction);
            }
            return RegistrationResult.Ok;
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

        private bool LoginDataIsValid(string loginname, string email)
        {
            return _repository.Entities.Where(u => u.Loginname == loginname || u.EMail == email).Count() == 0;
        }


        private Login GetExistingLogin(string loginname, string email)
        {
            return _repository.Entities.Where(e => e.Loginname == loginname && e.EMail == email).FirstOrDefault();
        }

        public RegistrationConfirmationResult ConfirmLogin(string loginname)
        {
            Login login = _repository.Entities.Where(e => e.Loginname == loginname).Single();
            if (login.Confirmed)
                return RegistrationConfirmationResult.Error;
            login.Confirm();
            _repository.SaveAllChanges();
            _remoteActionChamber.RemoveAction(ConfirmLoginAction, loginname);
            return RegistrationConfirmationResult.Confirmed;
        }

        public void StartPasswordReset(string email)
        {
            var login = _repository.Entities.Where(e => e.EMail == email).SingleOrDefault();
            if (login == null)
                return;
            if (!login.Confirmed)
            {
                var webAction = _remoteActionChamber.GetActiveAction(ConfirmLoginAction, login.Loginname);
                _mail.SendRegistrationConfirmationRequest(login, webAction);
            }
            else
            {
                var webAction = _remoteActionChamber.CreateAction(PasswordResetAction, email);
                _mail.SendPasswordResetRequest(login, webAction);
            }
        }

        public ChangePasswordResult ChangePassword(string loginname, string oldPassword, string newPassword,
                                                   string confirmPassword)
        {
            if (newPassword != confirmPassword)
                return ChangePasswordResult.PasswordMismatch;
            var login = _repository.Entities.Where(u => u.Loginname == loginname && u.Confirmed).SingleOrDefault();

            if (login == null)
                return ChangePasswordResult.Error;

            if (!login.IsPasswordValid(oldPassword, _hashing))
                return ChangePasswordResult.InvalidPassword;

            login.SetPassword(newPassword, _hashing);
            _repository.SaveAllChanges();

            return ChangePasswordResult.Ok;
        }

        public ChangePasswordResult SetPassword(string loginname, string newPassword,
                                                string confirmPassword)
        {
            if (newPassword != confirmPassword)
                return ChangePasswordResult.PasswordMismatch;

            var login = _repository.Entities.Where(u => u.Loginname == loginname && u.Confirmed).SingleOrDefault();
            if (login == null)
                return ChangePasswordResult.Error;

            var webAction = _remoteActionChamber.GetActiveAction(PasswordResetAction, login.EMail);
            if (webAction == null)
                return ChangePasswordResult.Error;

            login.SetPassword(newPassword, _hashing);
            _repository.SaveAllChanges();
            _remoteActionChamber.RemoveAction(PasswordResetAction, login.EMail);
            return ChangePasswordResult.Ok;
        }
    }
}