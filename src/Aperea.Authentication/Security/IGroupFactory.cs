using Aperea.EntityModels;

namespace Aperea.Security
{
    public interface IGroupFactory
    {
        LoginGroup GetGroup(string groupName);
    }
}