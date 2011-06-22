using System.Linq;
using Aperea.EntityModels;
using Aperea.Repositories;
using Aperea.Security;
using Aperea.Services;
using Aperea.Settings;

namespace Aperea.Data
{
    public class MembershipSeeder : IDatabaseSeeder
    {
        readonly IRepository<Login> _repository;
        readonly IHashing _hashing;
        readonly IMembershipSettings _settings;
        readonly IRoleFactory _roleFactory;
        readonly IGroupFactory _groupFactory;
        private const int CurrentVersion = 1;

        public MembershipSeeder(IRepository<Login> repository, IHashing hashing, IMembershipSettings settings, IRoleFactory roleFactory, IGroupFactory groupFactory)
        {
            _repository = repository;
            _hashing = hashing;
            _settings = settings;
            _roleFactory = roleFactory;
            _groupFactory = groupFactory;
        }

        public void Seed()
        {
            Migrate(0);
        }

        public int Order
        {
            get { return -29000; }
        }

        public int Migrate(int version)
        {
            if (version < 1)
            {
                var userRole = _roleFactory.GetRole(MembershipRoles.Authorized);
                var adminRole = _roleFactory.GetRole(MembershipRoles.Administrator);
                var userGroup = _groupFactory.GetGroup(MembershipGroups.Users);
                var adminGroup = _groupFactory.GetGroup(MembershipGroups.Administrators);
                userGroup.AddRole(userRole);
                adminGroup.AddRole(userRole);
                adminGroup.AddRole(adminRole);
                if (!string.IsNullOrEmpty(_settings.AdministratorLogin))
                {
                    if (!_repository.Entities.Any(l => l.Loginname == _settings.AdministratorLogin))
                    {
                        var login = new Login(_settings.AdministratorLogin, _settings.AdministratorEMail);
                        login.SetPassword(_settings.AdministratorPassword, _hashing);
                        login.Confirm();
                        login.AddGroup(adminGroup);
                        login.AddGroup(userGroup);
                        _repository.Add(login);
                    }
                }
                _repository.SaveAllChanges();                
            }
            return CurrentVersion;
        }
    }
}