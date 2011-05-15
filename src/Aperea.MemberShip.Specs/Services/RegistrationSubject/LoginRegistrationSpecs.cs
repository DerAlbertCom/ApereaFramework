using Aperea.EntityModels;
using Aperea.Repositories;
using Aperea.Services;
using Machine.Fakes;
using Machine.Specifications;

namespace Aperea.Specs.Services
{
    [Subject(typeof (Registration), "New User")]
    public class When_an_user_enters_username_email_and_same_passwords : WithSubject<Registration>
    {
        private Establish that =
            () =>
                {
                    With<BehaviorNoWebUsers>();
                    With<BehaviorUserRegistration>();
                };

        private Because of =
            () => result = Subject.RegisterNewUser("aweinert", "info@der-albert.com", "kennwort", "kennwort");

        private It should_the_result_is_ok = () => result.ShouldEqual(UserRegistrationResult.Ok);

        private It should_send_a_confirmation_email =
            () =>
            The<IUserRegistrationMail>().WasToldTo(
                m =>
                m.SendRegistrationConfirmation(Param<Login>.Matches(u => u.Username == "aweinert"),
                                               Param<RemoteAction>.Matches(a => a.Parameter == "aweinert")));

        private It should_create_a_user_in_the_database =
            () => The<IRepository<Login>>().WasToldTo(r => r.Add(
                Param<Login>.Matches(user =>
                                     (user.EMail == "info@der-albert.com" &&
                                      user.Username == "aweinert" &&
                                      user.PasswordHash == "hashkennwort")
                    )));

        private It should_create_a_registration_confirmation_action =
            () => The<IRemoteActionChamber>().WasToldTo(c => c.CreateAction(Registration.ConfirmWebUserAction, "aweinert"));

        private static UserRegistrationResult result;
    }

    [Subject(typeof (Registration), "New User")]
    public class When_a_user_enters_an_existing_username_with_existing_email_for_an_unconfirmed_account :
        WithSubject<Registration>
    {
        private Establish that = () =>
                                     {
                                         With<BehaviorExistingsUsers>();
                                         With<BehaviorUserRegistration>();
                                     };

        private Because of =
            () => result = Subject.RegisterNewUser("aweinert", "info@der-albert.com", "kennwort", "kennwort");

        private It should_the_result_is_ok = () => result.ShouldEqual(UserRegistrationResult.Ok);

        private It should_send_a_confirmation_email =
            () =>
            The<IUserRegistrationMail>().WasToldTo(
                m =>
                m.SendRegistrationConfirmation(Param<Login>.IsNotNull,
                                               Param<RemoteAction>.Matches(a => a.Parameter == "aweinert")));

        private It should_not_create_a_user_in_the_database =
            () => The<IRepository<Login>>().WasNotToldTo(r => r.Add(
                Param<Login>.Matches(user => user.Username == "aweinert")));

        private It should_not_create_a_registration_confirmation_action =
            () =>
            The<IRemoteActionChamber>().WasNotToldTo(c => c.CreateAction(Registration.ConfirmWebUserAction, "aweinert"));


        private static UserRegistrationResult result;
    }

    [Subject(typeof (Registration), "New User")]
    public class When_a_user_enters_an_existing_username : WithSubject<Registration>
    {
        private Establish that =
            () =>
                {
                    With<BehaviorExistingsUsers>();
                    With<BehaviorUserRegistration>();
                };

        private Because of =
            () => result = Subject.RegisterNewUser("aweinert", "foo@bar.de", "kennwort", "kennwort");

        private It should_the_result_should_invalid_userdata =
            () => result.ShouldEqual(UserRegistrationResult.InvalidUserdata);

        private It should_not_send_a_confirmation_email =
            () =>
            The<IUserRegistrationMail>().WasNotToldTo(
                m =>
                m.SendRegistrationConfirmation(
                    Param<Login>.Matches(u => u.Username == "aweinert"),
                    Param<RemoteAction>.Matches(a => a.Parameter == "aweinert"))
                );

        private It shoud_not_create_a_user_in_the_database =
            () => The<IRepository<Login>>().WasNotToldTo(r => r.Add(
                Param<Login>.Matches(user => user.Username == "aweinert")));

        private static UserRegistrationResult result;
    }

    [Subject(typeof (Registration), "New User")]
    public class When_a_user_enters_an_existing_email : WithSubject<Registration>
    {
        private Establish that =
            () =>
                {
                    With<BehaviorExistingsUsers>();
                    With<BehaviorUserRegistration>();
                };

        private Because of =
            () => result = Subject.RegisterNewUser("dieaertze", "albert.weinert@webrunners.de", "kennwort", "kennwort");

        private It should_the_result_should_invalid_userdata =
            () => result.ShouldEqual(UserRegistrationResult.InvalidUserdata);

        private It should_not_send_a_confirmation_email =
            () =>
            The<IUserRegistrationMail>().WasNotToldTo(
                m =>
                m.SendRegistrationConfirmation(
                    Param<Login>.Matches(u => u.Username == "dieaertze"),
                    Param<RemoteAction>.Matches(a => a.Parameter == "dieaertze"))
                );

        private It should_not_create_a_user_in_the_database =
            () => The<IRepository<Login>>().WasNotToldTo(r => r.Add(
                Param<Login>.Matches(user => user.Username == "dieaertze")));

        private static UserRegistrationResult result;
    }

    [Subject(typeof (Registration), "New User")]
    public class When_a_user_enters_an_username_and_different_passwords : WithSubject<Registration>
    {
        private Establish that =
            () =>
                {
                    With<BehaviorExistingsUsers>();
                    With<BehaviorUserRegistration>();
                };

        private Because of =
            () => result = Subject.RegisterNewUser("mensch", "meier@schmitz.de", "kennwort", "password");

        private It should_the_result_password_missmatch =
            () => result.ShouldEqual(UserRegistrationResult.PasswordMismatch);

        private It should_not_send_a_confirmation_email =
            () =>
            The<IUserRegistrationMail>().WasNotToldTo(
                m =>
                m.SendRegistrationConfirmation(
                    Param<Login>.Matches(u => u.Username == "mensch"),
                    Param<RemoteAction>.Matches(a => a.Parameter == "mensch"))
                );

        private It shoud_not_create_a_user_in_the_database =
            () => The<IRepository<Login>>().WasNotToldTo(r => r.Add(
                Param<Login>.Matches(user => user.Username == "mensch")));

        private static UserRegistrationResult result;
    }
}