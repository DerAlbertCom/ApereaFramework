using Aperea.EntityModels;
using Aperea.Services;
using Machine.Fakes;

namespace Aperea.Specs.Services
{
    internal class BehaviorUserRegistration
    {
        private OnEstablish _context = fakeAccessor =>
                                           {
                                               new FakeHashing(fakeAccessor);

                                               fakeAccessor.The<IWebActionChamber>()
                                                   .WhenToldTo(
                                                       c =>
                                                       c.CreateAction(Param<string>.IsNotNull, Param<string>.IsNotNull))
                                                   .
                                                   Return<string, string>(
                                                       (action, parameter) => new RemoteAction(action, parameter));

                                               fakeAccessor.The<IWebActionChamber>()
                                                   .WhenToldTo(
                                                       c =>
                                                       c.GetActiveAction(Param<string>.IsNotNull,
                                                                         Param<string>.IsNotNull)).
                                                   Return<string, string>(
                                                       (action, parameter) => new RemoteAction(action, parameter));
                                           };
    }
}