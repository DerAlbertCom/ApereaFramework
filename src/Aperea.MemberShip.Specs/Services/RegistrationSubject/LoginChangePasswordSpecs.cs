using Aperea.Services;
using Machine.Specifications;

namespace Aperea.Specs.Services
{
    [Subject(typeof (Registration), "Changing Password")]
    public class When_an_user_enters_matching_passwords : FakeSubject<Registration>
    {
        private Establish that = () =>
                                     {
                                         With<BehaviorRegistration>();
                                         _logins = new BehaviorExistingLogins(Accessor);
                                     };

        private Because of = () => result = Subject.ChangePassword("awn", "kennwort", "password", "password");

        private It should_save_the_new_password =
            () => _logins[1].PasswordHash.ShouldEqual("hashpassword");

        private It should_the_result_ok = () => result.ShouldEqual(ChangePasswordResult.Ok);

        private static ChangePasswordResult result;
        private static BehaviorExistingLogins _logins;
    }

    [Subject(typeof (Registration), "Changing Password")]
    public class When_an_user_enters_none_matching_passwords : FakeSubject<Registration>
    {
        private Establish that = () =>
                                     {
                                         With<BehaviorRegistration>();
                                         _logins = new BehaviorExistingLogins(Accessor);
                                     };

        private Because of = () => result = Subject.ChangePassword("awn", "kennwort", "musdf", "password");

        private It should_not_save_the_new_password =
            () => _logins[1].PasswordHash.ShouldEqual("hashkennwort");

        private It should_the_result_password_mismatch = () => result.ShouldEqual(ChangePasswordResult.PasswordMismatch);

        private static ChangePasswordResult result;
        private static BehaviorExistingLogins _logins;
    }

    [Subject(typeof (Registration), "Changing Password")]
    public class When_an_user_enters_a_wrong_old_password : FakeSubject<Registration>
    {
        private Establish that = () =>
                                     {
                                         With<BehaviorRegistration>();
                                         _logins = new BehaviorExistingLogins(Accessor);
                                     };

        private Because of = () => result = Subject.ChangePassword("awn", "mues", "password", "password");

        private It should_not_save_the_new_password =
            () => _logins[1].PasswordHash.ShouldEqual("hashkennwort");

        private It should_the_result_invalid_password = () => result.ShouldEqual(ChangePasswordResult.InvalidPassword);

        private static ChangePasswordResult result;
        private static BehaviorExistingLogins _logins;
    }
}