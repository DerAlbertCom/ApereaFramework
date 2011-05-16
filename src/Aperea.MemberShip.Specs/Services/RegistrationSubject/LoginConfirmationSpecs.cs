using Aperea.Services;
using Machine.Fakes;
using Machine.Specifications;

namespace Aperea.Specs.Services
{
    [Subject(typeof (Registration), "Login Confirmation")]
    public class When_a_login_is_up_to_confirmed : FakeSubject<Registration>
    {
        Establish that =
            () =>
            {
                _logins = new BehaviorExistingLogins(Accessor);
                With<BehaviorRegistration>();
            };

        Because of = () => result = Subject.ConfirmLogin("aweinert");

        It should_set_confirmed = () => _logins[0].Confirmed.ShouldBeTrue();
        It should_set_active = () => _logins[0].Active.ShouldBeTrue();
        It should_the_result_shoud_login_confirmed = () => result.ShouldEqual(RegistrationConfirmationResult.Confirmed);

        It should_delete_the_confirmation_key = () =>
                                                The<IRemoteActionChamber>()
                                                    .WasToldTo(
                                                        c =>
                                                        c.RemoveAction(Registration.ConfirmLoginAction,
                                                                       "aweinert"));

        static BehaviorExistingLogins _logins;
        static RegistrationConfirmationResult result;
    }
}