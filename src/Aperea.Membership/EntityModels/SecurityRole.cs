using System;
using System.ComponentModel.DataAnnotations;

namespace Aperea.EntityModels
{
    public class SecurityRole
    {
        protected SecurityRole()
        {
            
        }


        public SecurityRole(string roleName)
        {
            RoleName = roleName;
        }

        public int Id { get; set; }
        [Required]
        [StringLength(256)]
        public string RoleName { get; private set; }
    }
}