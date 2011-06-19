using System.Linq;
using Aperea.EntityModels;
using Aperea.Repositories;

namespace Aperea.Security
{
    public class GroupFactory : IGroupFactory
    {
        readonly IRepository<LoginGroup> _repository;

        public GroupFactory(IRepository<LoginGroup> repository)
        {
            _repository = repository;
        }

        public LoginGroup GetGroup(string groupName)
        {
            LoginGroup loginGroup = FindGroup(groupName);
            return loginGroup ?? CreateGroup(groupName);
        }

        LoginGroup CreateGroup(string groupName)
        {
            var group = new LoginGroup(groupName);
            _repository.Add(group);
            _repository.SaveAllChanges();
            return group;
        }

        LoginGroup FindGroup(string groupName)
        {
            return _repository.Entities.SingleOrDefault(g => g.GroupName == groupName);
        }
    }
}