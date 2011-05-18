using System;
using System.Linq;
using System.Collections.Generic;

namespace Aperea.EntityModels
{
    public class LoginGroup
    {
        protected LoginGroup()
        {
        }

        public LoginGroup(string groupName)
        {
            Roles = new HashSet<SecurityRole>();
            GroupName = groupName;
        }

        public int Id { get; set; }
        public string GroupName { get; set; }

        public void AddRole(SecurityRole role)
        {
            if (Roles.Contains(role))
                return;
            Roles.Add(role);
        }
        public ICollection<SecurityRole> Roles { get; set; }

        public void RemoveRole(SecurityRole role)
        {
            Roles.Remove(role);
        }

        public void SetRoles(IEnumerable<SecurityRole> newRoles)
        {
            Roles.Clear();
            foreach (var role in newRoles)
            {
                Roles.Add(role);
            }
        }
    }
}