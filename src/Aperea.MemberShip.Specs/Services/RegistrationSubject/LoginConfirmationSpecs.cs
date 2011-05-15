using Aperea.Services;
using Machine.Fakes;
using Machine.Specifications;

namespace Aperea.Specs.Services
{
    [Subject(typeof (Registration), "Login Confirmation")]
    public class When_a_login_is_up_to_confirmed : WithSubject<Registration>
    {
        private Establish that =
            () =>
                {
                    _logins = With<BehaviorExistingsLogins>();
                    With<BehaviorRegistration>();
                };

        private Because of = () => result = Subject.ConfirmLogin("aweinert");

        private It should_set_confirmed = () => _logins[0].Confirmed.ShouldBeTrue();
        private It should_set_active = () => _logins[0].Active.ShouldBeTrue();
        private It should_the_result_shoud_login_confirmed = () => result.ShouldEqual(RegistrationConfirmationResult.Confirmed);

        private It should_delete_the_confirmation_key = () =>
                                                        The<IRemoteActionChamber>()
                                                            .WasToldTo(
                                                                c =>
                                                                c.RemoveAction(Registration.ConfirmLoginAction,
                                                                               "aweinert"));

        private static BehaviorExistingsLogins _logins;
        private static RegistrationConfirmationResult result;
    }
}