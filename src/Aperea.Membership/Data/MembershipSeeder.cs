using Aperea.EntityModels;
using Aperea.Repositories;

namespace Aperea.Data
{
    public class MembershipSeeder : IDatabaseSeeder
    {
        readonly IRepository<LoginGroup> _repositoryGroups;

        public MembershipSeeder(IRepository<LoginGroup> repositoryGroups)
        {
            _repositoryGroups = repositoryGroups;
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
        }

        public int Order
        {
            get { return -29000; }
        }
    }
}