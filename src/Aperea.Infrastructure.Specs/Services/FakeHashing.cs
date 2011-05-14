using Aperea.Services;
using Machine.Fakes;

namespace Aperea.Specs.Services
{
    /// <summary>
    /// Returns a predictable Hash for testing, it simple adds "hash" in front of the password
    /// </summary>
    public class FakeHashing
    {
        public FakeHashing(IFakeAccessor fakeAccessor)
        {
            fakeAccessor.The<IHashing>()
                .WhenToldTo(h => h.GetHash(Param<string>.IsNotNull, Param<string>.IsNotNull))
                .Return<string>(password => "hash" + password);
        }
    }
}