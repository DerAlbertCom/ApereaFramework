using System;
using System.Linq;
using Aperea.EntityModels;
using Aperea.Repositories;

namespace Aperea.Services
{
    public class LoginValidation : ILoginValidation
    {
        readonly IRepository<Login> _repository;
        readonly IHashing _hashing;

        public LoginValidation(IRepository<Login> repository, IHashing hashing)
        {
            _repository = repository;
            _hashing = hashing;
        }

        public bool ValidateLoginForLogon(string loginName, string password)
        {
            var login = FindLogin(loginName);
            if (login == null)
                return false;

            if (!login.IsPasswordValid(password, _hashing))
            {
                return false;
            }

            login.LastLogin = DateTime.UtcNow;
            _repository.SaveAllChanges();
            return true;
        }

        public bool IsValidLogin(string loginName)
        {
            return FindLogin(loginName) != null;
        }

        Login FindLogin(string loginName)
        {
            var query = from l in _repository.Entities
                        where l.Active && l.Confirmed && l.Loginname == loginName
                        select l;
            return query.SingleOrDefault();
        }
    }
}