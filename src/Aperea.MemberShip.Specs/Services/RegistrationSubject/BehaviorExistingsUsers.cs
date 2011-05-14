using System.Collections.Generic;
using Aperea.EntityModels;
using Aperea.Services;
using Aperea.Specs.Repositories;
using Machine.Fakes;

namespace Aperea.Specs.Services
{
    internal class BehaviorExistingsUsers
    {
        static FakeRepository<Login> repository;

        OnEstablish _context =
            accessor => { repository = new FakeRepository<Login>(accessor, CreateUsers(accessor)); };

        static IList<Login> CreateUsers(IFakeAccessor accessor)
        {
            IList<Login> users = new List<Login> {
                new Login("aweinert", "info@der-albert.com"),
                new Login("awn", "albert.weinert@webrunners.de"),
                new Login("cvk", "christoph.vonkruechten@webrunners.de"),
                new Login("fm", "frank.muellers@webrunners.de")
            };
            users[1].Confirm();
            users[2].Confirm();
            users[0].SetPassword("kennwort", accessor.The<IHashing>());
            users[1].SetPassword("kennwort", accessor.The<IHashing>());
            users[2].SetPassword("kennwort", accessor.The<IHashing>());
            users[3].SetPassword("kennwort", accessor.The<IHashing>());
            return users;
        }

        public Login this[int index]
        {
            get { return repository[index]; }
        }
    }
}