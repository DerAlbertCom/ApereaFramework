using System;
using System.Linq;
using System.Security.Principal;
using Aperea.EntityModels;
using Aperea.MVC.StateProvider;
using Aperea.Repositories;
using Microsoft.Practices.ServiceLocation;

namespace Aperea.MVC.Security
{
    public class ApereaPrincipal : IPrincipal
    {
        readonly IStateStorage _state = new HttpContextStateStorage();

        public ApereaPrincipal()
        {
            Identity = new ApereaIdentity();
        }

        string[] _roles = new string[0];

        public ApereaPrincipal(IIdentity identity)
        {
            Identity = identity;
        }

        public ApereaPrincipal(Login login)
        {
            Identity = new ApereaIdentity(login);
        }

        public bool IsInRole(string role)
        {
            if (_roles.Length == 0)
            {
                _roles = GetRoles();
            }
            return Array.BinarySearch(_roles, role.ToLowerInvariant()) >= 0;
        }

        string[] GetRoles()
        {
            return _state.Get<string[]>("ApereaFramework.SecurityRoles", GetRolesFromDatabase);
        }

        string[] GetRolesFromDatabase()
        {
            var repository = ServiceLocator.Current.GetInstance<IRepository<Login>>();
            var query = from l in repository.Entities
                        where l.Loginname == Identity.Name && l.Active
                        let groups = l.Groups
                        from g in groups
                        let roles = g.Roles
                        from r in roles
                        select r.RoleName;

            return query.OrderBy(role => role).AsEnumerable().Select(role => role.ToLowerInvariant()).ToArray();
        }

        public IIdentity Identity { get; private set; }
    }
}