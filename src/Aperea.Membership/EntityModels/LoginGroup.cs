using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Aperea.EntityModels
{
    public class LoginGroup
    {
        protected LoginGroup()
        {
            Roles = new HashSet<SecurityRole>();
        }

        public LoginGroup(string groupName)
        {
            Roles = new HashSet<SecurityRole>();
            GroupName = groupName;
        }

        public int Id { get; set; }
        [Required]
        [StringLength(256)]
        public string GroupName { get; set; }

        public void AddRole(SecurityRole role)
        {
            if (Roles.Any(r=>r.RoleName==role.RoleName))
            {
                return;
            }
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