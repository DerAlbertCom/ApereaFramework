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
        Establish that =
            () =>
                {
                    With<BehaviorNoWebUsers>();
                    With<BehaviorUserRegistration>();
                };

        Because of = () => result = Subject.RegisterNewUser("aweinert", "info@der-albert.com", "kennwort", "kennwort");

        It should_the_result_is_ok = () => result.ShouldEqual(UserRegistrationResult.Ok);

        It should_send_a_confirmation_email =
            () =>
            The<IUserRegistrationMail>().WasToldTo(
                m =>
                m.SendRegistrationConfirmation(Param<Login>.Matches(u=>u.Username=="aweinert"),
                                               Param<RemoteAction>.Matches(a => a.Parameter == "aweinert")));

        It should_create_a_user_in_the_database =
            () => The<IRepository<Login>>().WasToldTo(r => r.Add(
                Param<Login>.Matches(user =>
                                        (user.EMail == "info@der-albert.com" &&
                                         user.Username == "aweinert" &&
                                         user.PasswordHash == "hashkennwort")
                    )));

        It should_create_a_registration_confirmation_action =
            () => The<IWebActionChamber>().WasToldTo(c => c.CreateAction(Registration.ConfirmWebUserAction, "aweinert"));

        static UserRegistrationResult result;
    }

    [Subject(typeof(Registration), "New User")]
    public class When_a_user_enters_an_existing_username_with_existing_email_for_an_unconfirmed_account :
        WithSubject<Registration>
    {
        Establish that = () =>
                             {
                                 With<BehaviorExistingsUsers>();
                                 With<BehaviorUserRegistration>();
                             };

        Because of =
            () => result = Subject.RegisterNewUser("aweinert", "info@der-albert.com", "kennwort", "kennwort");

        It should_the_result_is_ok = () => result.ShouldEqual(UserRegistrationResult.Ok);

        It should_send_a_confirmation_email =
            () =>
            The<IUserRegistrationMail>().WasToldTo(
                m =>
                m.SendRegistrationConfirmation(Param<Login>.IsNotNull,
                                               Param<RemoteAction>.Matches(a => a.Parameter == "aweinert")));

        It should_not_create_a_user_in_the_database =
            () => The<IRepository<Login>>().WasNotToldTo(r => r.Add(
                Param<Login>.Matches(user => user.Username == "aweinert")));

        It should_not_create_a_registration_confirmation_action =
            () => The<IWebActionChamber>().WasNotToldTo(c => c.CreateAction(Registration.ConfirmWebUserAction, "aweinert"));


        static UserRegistrationResult result;
    }

    [Subject(typeof(Registration), "New User")]
    public class When_a_user_enters_an_existing_username : WithSubject<Registration>
    {
        Establish that =
            () =>
                {
                    With<BehaviorExistingsUsers>();
                    With<BehaviorUserRegistration>();
                };

        Because of =
            () => result = Subject.RegisterNewUser("aweinert", "foo@bar.de", "kennwort", "kennwort");

        It should_the_result_should_invalid_userdata =
            () => result.ShouldEqual(UserRegistrationResult.InvalidUserdata);

        It should_not_send_a_confirmation_email =
            () =>
            The<IUserRegistrationMail>().WasNotToldTo(
                m =>
                m.SendRegistrationConfirmation(
                    Param<Login>.Matches(u => u.Username == "aweinert"),
                    Param<RemoteAction>.Matches(a => a.Parameter == "aweinert"))
                );

        It shoud_not_create_a_user_in_the_database =
            () => The<IRepository<Login>>().WasNotToldTo(r => r.Add(
                Param<Login>.Matches(user => user.Username == "aweinert")));

        static UserRegistrationResult result;
    }

    [Subject(typeof(Registration), "New User")]
    public class When_a_user_enters_an_existing_email : WithSubject<Registration>
    {
        Establish that =
            () =>
                {
                    With<BehaviorExistingsUsers>();
                    With<BehaviorUserRegistration>();
                };

        Because of =
            () => result = Subject.RegisterNewUser("dieaertze", "albert.weinert@webrunners.de", "kennwort", "kennwort");

        It should_the_result_should_invalid_userdata =
            () => result.ShouldEqual(UserRegistrationResult.InvalidUserdata);

        It should_not_send_a_confirmation_email =
            () =>
            The<IUserRegistrationMail>().WasNotToldTo(
                m =>
                m.SendRegistrationConfirmation(
                    Param<Login>.Matches(u => u.Username == "dieaertze"),
                    Param<RemoteAction>.Matches(a => a.Parameter == "dieaertze"))
                );

        It should_not_create_a_user_in_the_database =
            () => The<IRepository<Login>>().WasNotToldTo(r => r.Add(
                Param<Login>.Matches(user => user.Username == "dieaertze")));

        static UserRegistrationResult result;
    }

    [Subject(typeof(Registration), "New User")]
    public class When_a_user_enters_an_username_and_different_passwords : WithSubject<Registration>
    {
        Establish that =
            () =>
                {
                    With<BehaviorExistingsUsers>();
                    With<BehaviorUserRegistration>();
                };

        Because of =
            () => result = Subject.RegisterNewUser("mensch", "meier@schmitz.de", "kennwort", "password");

        It should_the_result_password_missmatch =
            () => result.ShouldEqual(UserRegistrationResult.PasswordMismatch);

        It should_not_send_a_confirmation_email =
            () =>
            The<IUserRegistrationMail>().WasNotToldTo(
                m =>
                m.SendRegistrationConfirmation(
                    Param<Login>.Matches(u => u.Username == "mensch"),
                    Param<RemoteAction>.Matches(a => a.Parameter == "mensch"))
                );

        It shoud_not_create_a_user_in_the_database =
            () => The<IRepository<Login>>().WasNotToldTo(r => r.Add(
                Param<Login>.Matches(user => user.Username == "mensch")));

        static UserRegistrationResult result;
    }
}