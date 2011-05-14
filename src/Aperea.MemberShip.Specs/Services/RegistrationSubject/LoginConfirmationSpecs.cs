using Aperea.Services;
using Machine.Fakes;
using Machine.Specifications;

namespace Aperea.Specs.Services
{
    [Subject(typeof (Registration), "User Confirmation")]
    public class When_an_user_is_up_confirmed : WithSubject<Registration>
    {
        private Establish that =
            () =>
                {
                    users = With<BehaviorExistingsUsers>();
                    With<BehaviorUserRegistration>();
                };

        private Because of = () => result = Subject.ConfirmUser("aweinert");

        private It should_set_confirmed = () => users[0].Confirmed.ShouldBeTrue();
        private It should_set_active = () => users[0].Active.ShouldBeTrue();
        private It should_the_result_shoud_user_confirmed = () => result.ShouldEqual(UserConfirmationResult.Confirmed);

        private It should_delete_the_confirmation_key = () =>
                                                        The<IWebActionChamber>()
                                                            .WasToldTo(
                                                                c =>
                                                                c.RemoveAction(Registration.ConfirmWebUserAction,
                                                                               "aweinert"));

        private static BehaviorExistingsUsers users;
        private static UserConfirmationResult result;
    }
}