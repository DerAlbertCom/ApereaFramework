using Aperea.EntityModels;

namespace Aperea.Security
{
    public interface IRoleFactory
    {
        SecurityRole GetRole(string roleName);
    }
}