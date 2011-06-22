using Aperea.EntityModels;
using Aperea.Repositories;
using Aperea.Services;
using Machine.Fakes;
using Machine.Specifications;

namespace Aperea.Specs.Services
{
    [Subject(typeof (Registration), "New Login")]
    public class When_an_user_enters_username_email_and_same_passwords : FakeSubject<Registration>
    {
        Establish that = () =>
        {
            With<BehaviorNoLogins>();
            With<BehaviorRegistration>();
        };

        Because of =
            () => result = Subject.RegisterNewLogin("aweinert", "info@der-albert.com", "kennwort", "kennwort");

        It should_the_result_is_ok = () => result.ShouldEqual(RegistrationResult.Ok);

        It should_send_a_confirmation_email = () =>
                                              The<IRegistrationMail>().WasToldTo(
                                                  m => m.SendRegistrationConfirmationRequest(
                                                      Param<Login>.Matches(u => u.Loginname == "aweinert"),
                                                      Param<RemoteAction>.Matches(a => a.Parameter == "aweinert")));

        It should_create_a_login_in_the_database = () =>
                                                   The<IRepository<Login>>().WasToldTo(r => r.Add(
                                                       Param<Login>.Matches(user =>
                                                                            (user.EMail == "info@der-albert.com" &&
                                                                             user.Loginname == "aweinert" &&
                                                                             user.PasswordHash == "hashkennwort")
                                                           )));

        It should_create_a_registration_confirmation_action =
            () =>
            The<IRemoteActionChamber>().WasToldTo(c => c.CreateAction(Registration.ConfirmLoginAction, "aweinert"));

        static RegistrationResult result;
    }

    [Subject(typeof (Registration), "New Login")]
    public class When_a_user_enters_an_existing_username_with_existing_email_for_an_unconfirmed_account :
        FakeSubject<Registration>
    {
        Establish that = () =>
        {
            new BehaviorExistingLogins(Accessor);

            With<BehaviorRegistration>();
        };

        Because of =
            () => result = Subject.RegisterNewLogin("aweinert", "info@der-albert.com", "kennwort", "kennwort");

        It should_the_result_is_ok = () => result.ShouldEqual(RegistrationResult.Ok);

        It should_send_a_confirmation_email =
            () =>
            The<IRegistrationMail>().WasToldTo(
                m =>
                m.SendRegistrationConfirmationRequest(Param<Login>.IsNotNull,
                                                      Param<RemoteAction>.Matches(a => a.Parameter == "aweinert")));

        It should_not_create_a_login_in_the_database =
            () => The<IRepository<Login>>().WasNotToldTo(r => r.Add(
                Param<Login>.Matches(user => user.Loginname == "aweinert")));

        It should_not_create_a_registration_confirmation_action =
            () =>
            The<IRemoteActionChamber>().WasNotToldTo(c => c.CreateAction(Registration.ConfirmLoginAction, "aweinert"));


        static RegistrationResult result;
    }

    [Subject(typeof (Registration), "New Login")]
    public class When_a_user_enters_an_existing_username : FakeSubject<Registration>
    {
        Establish that =
            () =>
            {
                new BehaviorExistingLogins(Accessor);
                With<BehaviorRegistration>();
            };

        Because of =
            () => result = Subject.RegisterNewLogin("aweinert", "foo@bar.de", "kennwort", "kennwort");

        It should_the_result_should_invalid_userdata =
            () => result.ShouldEqual(RegistrationResult.InvalidLoginData);

        It should_not_send_a_confirmation_email =
            () =>
            The<IRegistrationMail>().WasNotToldTo(
                m =>
                m.SendRegistrationConfirmationRequest(
                    Param<Login>.Matches(u => u.Loginname == "aweinert"),
                    Param<RemoteAction>.Matches(a => a.Parameter == "aweinert"))
                );

        It shoud_not_create_a_login_in_the_database =
            () => The<IRepository<Login>>().WasNotToldTo(r => r.Add(
                Param<Login>.Matches(user => user.Loginname == "aweinert")));

        static RegistrationResult result;
    }

    [Subject(typeof (Registration), "New Login")]
    public class When_a_user_enters_an_existing_email : FakeSubject<Registration>
    {
        Establish that =
            () =>
            {
                new BehaviorExistingLogins(Accessor);
                With<BehaviorRegistration>();
            };

        Because of =
            () =>
            result = Subject.RegisterNewLogin("dieaertze", "albert.weinert@awn-design.biz", "kennwort", "kennwort");

        It should_the_result_should_invalid_userdata =
            () => result.ShouldEqual(RegistrationResult.InvalidLoginData);

        It should_not_send_a_confirmation_email =
            () =>
            The<IRegistrationMail>().WasNotToldTo(
                m =>
                m.SendRegistrationConfirmationRequest(
                    Param<Login>.Matches(u => u.Loginname == "dieaertze"),
                    Param<RemoteAction>.Matches(a => a.Parameter == "dieaertze"))
                );

        It should_not_create_a_login_in_the_database =
            () => The<IRepository<Login>>().WasNotToldTo(r => r.Add(
                Param<Login>.Matches(user => user.Loginname == "dieaertze")));

        static RegistrationResult result;
    }

    [Subject(typeof (Registration), "New Login")]
    public class When_a_user_enters_an_username_and_different_passwords : FakeSubject<Registration>
    {
        Establish that =
            () =>
            {
                new BehaviorExistingLogins(Accessor);
                With<BehaviorRegistration>();
            };

        Because of =
            () => result = Subject.RegisterNewLogin("mensch", "meier@schmitz.de", "kennwort", "password");

        It should_the_result_password_missmatch =
            () => result.ShouldEqual(RegistrationResult.PasswordMismatch);

        It should_not_send_a_confirmation_email =
            () =>
            The<IRegistrationMail>().WasNotToldTo(
                m =>
                m.SendRegistrationConfirmationRequest(
                    Param<Login>.Matches(u => u.Loginname == "mensch"),
                    Param<RemoteAction>.Matches(a => a.Parameter == "mensch"))
                );

        It shoud_not_create_a_login_in_the_database =
            () => The<IRepository<Login>>().WasNotToldTo(r => r.Add(
                Param<Login>.Matches(login => login.Loginname == "mensch")));

        static RegistrationResult result;
    }
}