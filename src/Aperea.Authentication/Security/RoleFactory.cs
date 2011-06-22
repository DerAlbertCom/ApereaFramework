using System.Linq;
using Aperea.EntityModels;
using Aperea.Repositories;

namespace Aperea.Security
{
    public class RoleFactory : IRoleFactory
    {
        readonly IRepository<SecurityRole> _repository;

        public RoleFactory(IRepository<SecurityRole> repository)
        {
            _repository = repository;
        }

        public SecurityRole GetRole(string roleName)
        {
            SecurityRole role = FindRole(roleName);
            return role ?? CreateRole(roleName);
        }

        SecurityRole CreateRole(string roleName)
        {
            var role = new SecurityRole(roleName);
            _repository.Add(role);
            _repository.SaveAllChanges();
            return role;
        }

        SecurityRole FindRole(string roleName)
        {
            return _repository.Entities.SingleOrDefault(r => r.RoleName == roleName);
        }
    }
}