using System;
using System.Linq;
using System.Security.Principal;
using Aperea.EntityModels;
using Aperea.MVC.StateProvider;
using Aperea.Repositories;
using Aperea.Services;
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
            var rolesFinder = ServiceLocator.Current.GetInstance<IRolesFinder>();
            return rolesFinder.GetRolesForIdentity(Identity).ToArray();
        }

        public IIdentity Identity { get; private set; }
    }
}