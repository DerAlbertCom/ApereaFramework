using System;
using System.Linq;
using System.Web.Security;
using Aperea.EntityModels;
using Aperea.Repositories;
using Microsoft.Practices.ServiceLocation;

namespace Aperea.MVC.Security
{
    public class ApereaRoleProvider : RoleProvider
    {
        IQueryable<Login> LoginEntities()
        {
            return ServiceLocator.Current.GetInstance<IRepository<Login>>().Entities;
        }

        IQueryable<SecurityRole> RoleEntities()
        {
            return ServiceLocator.Current.GetInstance<IRepository<SecurityRole>>().Entities;
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            var query = from l in LoginEntities()
                        where l.Loginname == username
                        let groups = l.Groups
                        from g in groups
                        let roles = g.Roles
                        from r in roles
                        where r.RoleName == roleName
                        select r;
            return query.Any();
        }

        public override string[] GetRolesForUser(string username)
        {
            var query = from l in LoginEntities()
                        where l.Loginname == username 
                        let groups = l.Groups
                        from g in groups
                        let roles = g.Roles
                        from r in roles
                        select r.RoleName;

            return query.ToArray();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            var query = from r in RoleEntities()
                        orderby r.RoleName
                        select r.RoleName;
            return query.ToArray();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName
        {
            get { return ""; }
            set { throw new NotImplementedException(); }
        }
    }
}