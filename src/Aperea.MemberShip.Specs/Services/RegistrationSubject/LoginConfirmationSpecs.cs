using Aperea.Services;
using Machine.Fakes;
using Machine.Specifications;

namespace Aperea.Specs.Services
{
    [Subject(typeof (Registration), "User Confirmation")]
    public class When_an_user_is_up_confirmed : WithSubject<Registration>
    {
        Establish that =
            () => {
                users = With<BehaviorExistingsUsers>();
                With<BehaviorUserRegistration>();
            };

        Because of = () => result = Subject.ConfirmUser("aweinert");

        It should_set_confirmed = () => users[0].Confirmed.ShouldBeTrue();
        It should_set_active = () => users[0].Active.ShouldBeTrue();
        It should_the_result_shoud_user_confirmed = () => result.ShouldEqual(UserConfirmationResult.Confirmed);

        It should_delete_the_confirmation_key = () =>
                                                The<IWebActionChamber>()
                                                    .WasToldTo(
                                                        c => c.RemoveAction(Registration.ConfirmWebUserAction, "aweinert"));

        static BehaviorExistingsUsers users;
        static UserConfirmationResult result;
    }
}