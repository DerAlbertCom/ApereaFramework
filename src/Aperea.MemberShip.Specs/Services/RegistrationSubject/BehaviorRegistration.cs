using Aperea.EntityModels;
using Aperea.Services;
using Machine.Fakes;

namespace Aperea.Specs.Services
{
    internal class BehaviorRegistration
    {
        OnEstablish _context = fakeAccessor =>
        {
            new FakeHashing(fakeAccessor);

            fakeAccessor.The<IRemoteActionChamber>()
                .WhenToldTo(
                    c =>
                    c.CreateAction(Param<string>.IsNotNull, Param<string>.IsNotNull))
                .
                Return<string, string>(
                    (action, parameter) => new RemoteAction(action, parameter));

            fakeAccessor.The<IRemoteActionChamber>()
                .WhenToldTo(
                    c =>
                    c.GetActiveAction(Param<string>.IsNotNull,
                                      Param<string>.IsNotNull)).
                Return<string, string>(
                    (action, parameter) => new RemoteAction(action, parameter));
        };
    }
}