using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using Aperea.EntityModels;
using Aperea.Repositories;

namespace Aperea.Services
{
    public class RolesFinder : IRolesFinder
    {
        readonly IRepository<Login> _repository;

        public RolesFinder(IRepository<Login> repository)
        {
            _repository = repository;
        }

        public IEnumerable<string> GetRolesForIdentity(IIdentity identity)
        {
            var query = from l in _repository.Entities
                        where l.Loginname == identity.Name && l.Active
                        let groups = l.Groups
                        from g in groups
                        let roles = g.Roles
                        from r in roles
                        select r.RoleName;

            return query.OrderBy(role => role).AsEnumerable().Select(role => role.ToLowerInvariant());
        }
    }
}