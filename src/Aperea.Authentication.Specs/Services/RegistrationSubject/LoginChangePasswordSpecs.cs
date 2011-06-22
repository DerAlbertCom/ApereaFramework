using Aperea.Services;
using Machine.Specifications;

namespace Aperea.Specs.Services
{
    [Subject(typeof (Registration), "Changing Password")]
    public class When_an_user_enters_matching_passwords : FakeSubject<Registration>
    {
        Establish that = () =>
        {
            With<BehaviorRegistration>();
            _logins = new BehaviorExistingLogins(Accessor);
        };

        Because of = () => _result = Subject.ChangePassword("awn", "kennwort", "password", "password");

        It should_save_the_new_password = () => _logins[1].PasswordHash.ShouldEqual("hashpassword");

        It should_the_result_ok = () => _result.ShouldEqual(ChangePasswordResult.Ok);

        static ChangePasswordResult _result;
        static BehaviorExistingLogins _logins;
    }

    [Subject(typeof (Registration), "Changing Password")]
    public class When_an_user_enters_none_matching_passwords : FakeSubject<Registration>
    {
        Establish that = () =>
        {
            With<BehaviorRegistration>();
            _logins = new BehaviorExistingLogins(Accessor);
        };

        Because of = () => result = Subject.ChangePassword("awn", "kennwort", "musdf", "password");

        It should_not_save_the_new_password =
            () => _logins[1].PasswordHash.ShouldEqual("hashkennwort");

        It should_the_result_password_mismatch = () => result.ShouldEqual(ChangePasswordResult.PasswordMismatch);

        static ChangePasswordResult result;
        static BehaviorExistingLogins _logins;
    }

    [Subject(typeof (Registration), "Changing Password")]
    public class When_an_user_enters_a_wrong_old_password : FakeSubject<Registration>
    {
        Establish that = () =>
        {
            With<BehaviorRegistration>();
            _logins = new BehaviorExistingLogins(Accessor);
        };

        Because of = () => result = Subject.ChangePassword("awn", "mues", "password", "password");

        It should_not_save_the_new_password =
            () => _logins[1].PasswordHash.ShouldEqual("hashkennwort");

        It should_the_result_invalid_password = () => result.ShouldEqual(ChangePasswordResult.InvalidPassword);

        static ChangePasswordResult result;
        static BehaviorExistingLogins _logins;
    }
}