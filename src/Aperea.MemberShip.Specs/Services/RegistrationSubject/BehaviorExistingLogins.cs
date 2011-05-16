using System.Collections.Generic;
using Aperea.EntityModels;
using Aperea.Services;
using Aperea.Specs.Repositories;
using Machine.Fakes;

namespace Aperea.Specs.Services
{
    internal class BehaviorExistingLogins
    {
        readonly FakeRepository<Login> _repository;

        public BehaviorExistingLogins(IFakeAccessor accessor)
        {
            _repository = new FakeRepository<Login>(accessor, CreateLogins(accessor));
        }

        IEnumerable<Login> CreateLogins(IFakeAccessor accessor)
        {
            IList<Login> logins = new List<Login>
                                  {
                                      new Login("aweinert", "info@der-albert.com"),
                                      new Login("awn", "albert.weinert@awn-design.biz"),
                                      new Login("cvk", "christoph.vonkruechten@awn-design.biz"),
                                      new Login("fm", "frank.muellers@awn-design.biz")
                                  };
            logins[1].Confirm();
            logins[2].Confirm();
            logins[0].SetPassword("kennwort", accessor.The<IHashing>());
            logins[1].SetPassword("kennwort", accessor.The<IHashing>());
            logins[2].SetPassword("kennwort", accessor.The<IHashing>());
            logins[3].SetPassword("kennwort", accessor.The<IHashing>());
            return logins;
        }


        public Login this[int index]
        {
            get { return _repository[index]; }
        }
    }
}