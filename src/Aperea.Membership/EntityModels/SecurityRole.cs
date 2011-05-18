using System;

namespace Aperea.EntityModels
{
    public class SecurityRole
    {
        public SecurityRole(string roleName)
        {
            RoleName = roleName;
        }

        public string RoleName { get; private set; }
    }
}