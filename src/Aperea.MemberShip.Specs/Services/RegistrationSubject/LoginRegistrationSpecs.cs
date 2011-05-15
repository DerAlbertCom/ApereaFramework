using Aperea.EntityModels;
using Aperea.Repositories;
using Aperea.Services;
using Machine.Fakes;
using Machine.Specifications;

namespace Aperea.Specs.Services
{
    [Subject(typeof (Registration), "New Login")]
    public class When_an_user_enters_username_email_and_same_passwords : WithSubject<Registration>
    {
        private Establish that =
            () =>
                {
                    With<BehaviorNoLogins>();
                    With<BehaviorRegistration>();
                };

        private Because of =
            () => result = Subject.RegisterNewLogin("aweinert", "info@der-albert.com", "kennwort", "kennwort");

        private It should_the_result_is_ok = () => result.ShouldEqual(RegistrationResult.Ok);

        private It should_send_a_confirmation_email =
            () =>
            The<IRegistrationMail>().WasToldTo(
                m =>
                m.SendRegistrationConfirmationRequest(Param<Login>.Matches(u => u.Loginname == "aweinert"),
                                               Param<RemoteAction>.Matches(a => a.Parameter == "aweinert")));

        private It should_create_a_login_in_the_database =
            () => The<IRepository<Login>>().WasToldTo(r => r.Add(
                Param<Login>.Matches(user =>
                                     (user.EMail == "info@der-albert.com" &&
                                      user.Loginname == "aweinert" &&
                                      user.PasswordHash == "hashkennwort")
                    )));

        private It should_create_a_registration_confirmation_action =
            () => The<IRemoteActionChamber>().WasToldTo(c => c.CreateAction(Registration.ConfirmLoginAction, "aweinert"));

        private static RegistrationResult result;
    }

    [Subject(typeof (Registration), "New Login")]
    public class When_a_user_enters_an_existing_username_with_existing_email_for_an_unconfirmed_account :
        WithSubject<Registration>
    {
        private Establish that = () =>
                                     {
                                         With<BehaviorExistingsLogins>();
                                         With<BehaviorRegistration>();
                                     };

        private Because of =
            () => result = Subject.RegisterNewLogin("aweinert", "info@der-albert.com", "kennwort", "kennwort");

        private It should_the_result_is_ok = () => result.ShouldEqual(RegistrationResult.Ok);

        private It should_send_a_confirmation_email =
            () =>
            The<IRegistrationMail>().WasToldTo(
                m =>
                m.SendRegistrationConfirmationRequest(Param<Login>.IsNotNull,
                                               Param<RemoteAction>.Matches(a => a.Parameter == "aweinert")));

        private It should_not_create_a_login_in_the_database =
            () => The<IRepository<Login>>().WasNotToldTo(r => r.Add(
                Param<Login>.Matches(user => user.Loginname == "aweinert")));

        private It should_not_create_a_registration_confirmation_action =
            () =>
            The<IRemoteActionChamber>().WasNotToldTo(c => c.CreateAction(Registration.ConfirmLoginAction, "aweinert"));


        private static RegistrationResult result;
    }

    [Subject(typeof (Registration), "New Login")]
    public class When_a_user_enters_an_existing_username : WithSubject<Registration>
    {
        private Establish that =
            () =>
                {
                    With<BehaviorExistingsLogins>();
                    With<BehaviorRegistration>();
                };

        private Because of =
            () => result = Subject.RegisterNewLogin("aweinert", "foo@bar.de", "kennwort", "kennwort");

        private It should_the_result_should_invalid_userdata =
            () => result.ShouldEqual(RegistrationResult.InvalidLoginData);

        private It should_not_send_a_confirmation_email =
            () =>
            The<IRegistrationMail>().WasNotToldTo(
                m =>
                m.SendRegistrationConfirmationRequest(
                    Param<Login>.Matches(u => u.Loginname == "aweinert"),
                    Param<RemoteAction>.Matches(a => a.Parameter == "aweinert"))
                );

        private It shoud_not_create_a_login_in_the_database =
            () => The<IRepository<Login>>().WasNotToldTo(r => r.Add(
                Param<Login>.Matches(user => user.Loginname == "aweinert")));

        private static RegistrationResult result;
    }

    [Subject(typeof (Registration), "New Login")]
    public class When_a_user_enters_an_existing_email : WithSubject<Registration>
    {
        private Establish that =
            () =>
                {
                    With<BehaviorExistingsLogins>();
                    With<BehaviorRegistration>();
                };

        private Because of =
            () => result = Subject.RegisterNewLogin("dieaertze", "albert.weinert@awn-design.biz", "kennwort", "kennwort");

        private It should_the_result_should_invalid_userdata =
            () => result.ShouldEqual(RegistrationResult.InvalidLoginData);

        private It should_not_send_a_confirmation_email =
            () =>
            The<IRegistrationMail>().WasNotToldTo(
                m =>
                m.SendRegistrationConfirmationRequest(
                    Param<Login>.Matches(u => u.Loginname == "dieaertze"),
                    Param<RemoteAction>.Matches(a => a.Parameter == "dieaertze"))
                );

        private It should_not_create_a_login_in_the_database =
            () => The<IRepository<Login>>().WasNotToldTo(r => r.Add(
                Param<Login>.Matches(user => user.Loginname == "dieaertze")));

        private static RegistrationResult result;
    }

    [Subject(typeof (Registration), "New Login")]
    public class When_a_user_enters_an_username_and_different_passwords : WithSubject<Registration>
    {
        private Establish that =
            () =>
                {
                    With<BehaviorExistingsLogins>();
                    With<BehaviorRegistration>();
                };

        private Because of =
            () => result = Subject.RegisterNewLogin("mensch", "meier@schmitz.de", "kennwort", "password");

        private It should_the_result_password_missmatch =
            () => result.ShouldEqual(RegistrationResult.PasswordMismatch);

        private It should_not_send_a_confirmation_email =
            () =>
            The<IRegistrationMail>().WasNotToldTo(
                m =>
                m.SendRegistrationConfirmationRequest(
                    Param<Login>.Matches(u => u.Loginname == "mensch"),
                    Param<RemoteAction>.Matches(a => a.Parameter == "mensch"))
                );

        private It shoud_not_create_a_login_in_the_database =
            () => The<IRepository<Login>>().WasNotToldTo(r => r.Add(
                Param<Login>.Matches(login => login.Loginname == "mensch")));

        private static RegistrationResult result;
    }
}