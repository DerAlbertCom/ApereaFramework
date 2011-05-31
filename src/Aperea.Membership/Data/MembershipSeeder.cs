using Aperea.EntityModels;
using Aperea.Repositories;
using Aperea.Services;
using Aperea.Settings;

namespace Aperea.Data
{
    public class MembershipSeeder : IDatabaseSeeder
    {
        readonly IRepository<LoginGroup> _repositoryGroups;
        readonly IRepository<Login> _repository;
        readonly IHashing _hashing;
        readonly IMembershipSettings _settings;

        public MembershipSeeder(IRepository<LoginGroup> repositoryGroups, IRepository<Login> repository, IHashing hashing, IMembershipSettings settings)
        {
            _repositoryGroups = repositoryGroups;
            _repository = repository;
            _hashing = hashing;
            _settings = settings;
        }

        public void Seed()
        {
            var userRole = new SecurityRole("Authorized");
            var adminRole = new SecurityRole("Administrator");
            var userGroup = new LoginGroup("Users");
            var adminGroup = new LoginGroup("Administrators");
            userGroup.AddRole(userRole);
            adminGroup.AddRole(userRole);
            adminGroup.AddRole(adminRole);
            _repositoryGroups.Add(adminGroup);
            _repositoryGroups.Add(userGroup);
            var login = new Login(_settings.AdministratorLogin, _settings.AdministratorEMail);
            login.SetPassword(_settings.AdministratorPassword, _hashing);
            login.Confirm();
            login.AddGroup(adminGroup);
            login.AddGroup(userGroup);
            _repository.Add(login);
        }

        public int Order
        {
            get { return -29000; }
        }
    }
}