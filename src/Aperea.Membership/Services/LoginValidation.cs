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

            if (login.IsPasswordValid(password, _hashing))
            {
                login.LastLogin = DateTime.UtcNow;
                _repository.SaveAllChanges();
                return true;
            }
            return false;
        }

        Login FindLogin(string username)
        {
            var query = from l in _repository.Entities
                        where l.Active && l.Confirmed && l.Loginname == username
                        select l;
            return query.SingleOrDefault();
        }
    }
}