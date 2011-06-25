using System.Collections.Generic;
using System.Security.Principal;

namespace Aperea.Services
{
    public interface IRolesFinder
    {
        IEnumerable<string> GetRolesForIdentity(IIdentity identity);
    }
}