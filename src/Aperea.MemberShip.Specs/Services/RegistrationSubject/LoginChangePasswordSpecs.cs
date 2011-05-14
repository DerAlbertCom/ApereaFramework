using Aperea.Services;
using Machine.Fakes;
using Machine.Specifications;

namespace Aperea.Specs.Services
{

    [Subject(typeof (Registration), "Changing Password")]
    public class When_an_user_enters_matching_passwords : WithSubject<Registration>
    {
        Establish that = () => {
                             With<BehaviorUserRegistration>();
                             users = With<BehaviorExistingsUsers>();
                         };

        Because of = () => result = Subject.ChangePassword("awn", "kennwort", "password", "password");

        It should_save_the_new_password =
            () => users[1].PasswordHash.ShouldEqual("hashpassword");

        It should_the_result_ok = () => result.ShouldEqual(ChangePasswordResult.Ok);

        static ChangePasswordResult result;
        static BehaviorExistingsUsers users;
    }

    [Subject(typeof (Registration), "Changing Password")]
    public class When_an_user_enters_none_matching_passwords : WithSubject<Registration>
    {
        Establish that = () => {
                             With<BehaviorUserRegistration>();
                             users = With<BehaviorExistingsUsers>();
                         };

        Because of = () => result = Subject.ChangePassword("awn", "kennwort", "musdf", "password");

        It should_not_save_the_new_password =
            () => users[1].PasswordHash.ShouldEqual("hashkennwort");

        It should_the_result_password_mismatch = () => result.ShouldEqual(ChangePasswordResult.PasswordMismatch);

        static ChangePasswordResult result;
        static BehaviorExistingsUsers users;
    }

    [Subject(typeof (Registration), "Changing Password")]
    public class When_an_user_enters_a_wrong_old_password : WithSubject<Registration>
    {
        Establish that = () => {
                             With<BehaviorUserRegistration>();
                             users = With<BehaviorExistingsUsers>();
                         };

        Because of = () => result = Subject.ChangePassword("awn", "mues", "password", "password");

        It should_not_save_the_new_password =
            () => users[1].PasswordHash.ShouldEqual("hashkennwort");

        It should_the_result_invalid_password = () => result.ShouldEqual(ChangePasswordResult.InvalidPassword);

        static ChangePasswordResult result;
        static BehaviorExistingsUsers users;
    }
}