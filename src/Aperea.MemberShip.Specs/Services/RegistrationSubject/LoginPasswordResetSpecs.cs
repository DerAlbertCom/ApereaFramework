using Aperea.EntityModels;
using Aperea.Services;
using Machine.Fakes;
using Machine.Specifications;

namespace Aperea.Specs.Services
{
    [Subject(typeof (Registration), "Password reset request")]
    public class When_an_user_start_resetting_his_password_with_his_email : WithSubject<Registration>
    {
        private Establish that = () =>
                                     {
                                         With<BehaviorExistingsUsers>();
                                         With<BehaviorUserRegistration>();
                                     };

        private Because of = () => Subject.StartPasswordReset(email);

        private It should_send_a_password_reset_mail =
            () => The<IUserRegistrationMail>()
                      .WasToldTo(
                          m =>
                          m.SendPasswordResetRequest(Param<Login>.Matches(u => u.EMail == email),
                                                     Param<RemoteAction>.Matches(
                                                         a =>
                                                         a.Action == Registration.PasswordResetAction &&
                                                         a.Parameter == email)));

        private It should_generate_a_confirmation_key =
            () =>
            The<IRemoteActionChamber>().WasToldTo(c => c.CreateAction(Registration.PasswordResetAction, email));

        private static string email = "albert.weinert@webrunners.de";
    }

    [Subject(typeof (Registration), "Password reset request")]
    public class When_an_user_start_resetting_his_password_with_an_unknown_email : WithSubject<Registration>
    {
        private Establish that = () =>
                                     {
                                         With<BehaviorExistingsUsers>();
                                         With<BehaviorUserRegistration>();
                                     };

        private Because of = () => Subject.StartPasswordReset(email);

        private It should_not_send_a_password_reset_mail =
            () => The<IUserRegistrationMail>()
                      .WasNotToldTo(
                          m =>
                          m.SendPasswordResetRequest(Param<Login>.Matches(u => u.EMail == email),
                                                     Param<RemoteAction>.Matches(
                                                         a =>
                                                         a.Action == Registration.PasswordResetAction &&
                                                         a.Parameter == email)));

        private It should_not_generate_a_confirmation_key =
            () =>
            The<IRemoteActionChamber>().WasNotToldTo(c => c.CreateAction(Registration.PasswordResetAction, email));

        private It should_not_delete_the_confirmation_key =
            () =>
            The<IRemoteActionChamber>().WasNotToldTo(
                c => c.RemoveAction(Registration.PasswordResetAction, email));

        private static string email = "nomail@there.de";
    }

    [Subject(typeof (Registration), "Password reset request")]
    public class When_an_unconfirmed_user_start_resetting_his_password_with_his_email : WithSubject<Registration>
    {
        private Establish that = () =>
                                     {
                                         With<BehaviorExistingsUsers>();
                                         With<BehaviorUserRegistration>();
                                     };

        private Because of = () => Subject.StartPasswordReset(email);

        private It should_send_a_confirmation_email =
            () =>
            The<IUserRegistrationMail>().WasToldTo(
                m =>
                m.SendRegistrationConfirmation(Param<Login>.Matches(u => u.Username == "aweinert"),
                                               Param<RemoteAction>.Matches(
                                                   a =>
                                                   a.Parameter == "aweinert" &&
                                                   a.Action == Registration.ConfirmWebUserAction)));

        private It should_not_send_a_password_reset_mail =
            () => The<IUserRegistrationMail>()
                      .WasNotToldTo(
                          m =>
                          m.SendPasswordResetRequest(Param<Login>.Matches(u => u.EMail == email),
                                                     Param<RemoteAction>.Matches(
                                                         a =>
                                                         a.Action == Registration.PasswordResetAction &&
                                                         a.Parameter == email)));

        private It should_not_generate_a_confirmation_key =
            () =>
            The<IRemoteActionChamber>().WasNotToldTo(c => c.CreateAction(Registration.PasswordResetAction, email));

        private static string email = "info@der-albert.com";
    }

    [Subject(typeof (Registration), "Entering new Password on reset")]
    public class When_an_user_enters_matching_passwords_while_resetting_his_password : WithSubject<Registration>
    {
        private Establish that = () =>
                                     {
                                         With<BehaviorUserRegistration>();
                                         users = With<BehaviorExistingsUsers>();
                                     };

        private Because of = () => result = Subject.SetPassword("awn", "password", "password");

        private It should_save_the_new_password =
            () => users[1].PasswordHash.ShouldEqual("hashpassword");

        private It should_test_if_the_confirmation_key_exists =
            () =>
            The<IRemoteActionChamber>().WasToldTo(
                c => c.GetActiveAction(Registration.PasswordResetAction, "albert.weinert@webrunners.de"));

        private It should_delete_the_confirmation_key =
            () =>
            The<IRemoteActionChamber>().WasToldTo(
                c => c.RemoveAction(Registration.PasswordResetAction, "albert.weinert@webrunners.de"));

        private It should_the_result_ok = () => result.ShouldEqual(ChangePasswordResult.Ok);

        private static ChangePasswordResult result;
        private static BehaviorExistingsUsers users;
    }

    [Subject(typeof (Registration), "Entering new Password on reset")]
    public class When_an_user_enters_none_matching_passwords_while_resetting_his_password : WithSubject<Registration>
    {
        private Establish that = () =>
                                     {
                                         With<BehaviorUserRegistration>();
                                         users = With<BehaviorExistingsUsers>();
                                     };

        private Because of = () => result = Subject.SetPassword("awn", "musdf", "password");

        private It should_not_save_the_new_password =
            () => users[1].PasswordHash.ShouldEqual("hashkennwort");

        private It should_not_delete_the_confirmation_key =
            () =>
            The<IRemoteActionChamber>().WasNotToldTo(
                c => c.RemoveAction(Registration.PasswordResetAction, "albert.weinert@webrunners.de"));

        private It should_the_result_password_mismatch = () => result.ShouldEqual(ChangePasswordResult.PasswordMismatch);

        private static ChangePasswordResult result;
        private static BehaviorExistingsUsers users;
    }
}