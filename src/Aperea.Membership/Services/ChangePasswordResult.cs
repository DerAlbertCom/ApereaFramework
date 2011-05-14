using System.Resources;

namespace Aperea.Services
{
    public class ChangePasswordResult : Enumeration<int>
    {
        public static readonly ChangePasswordResult Error = new ChangePasswordResult(1, "ChangePasswordResult_Error");
        public static readonly ChangePasswordResult Ok = new ChangePasswordResult(0, "Ok");

        public static readonly ChangePasswordResult InvalidPassword = new ChangePasswordResult(2,
                                                                                               "UserRegistrationResult_PasswordMismatch");

        public static readonly ChangePasswordResult PasswordMismatch = new ChangePasswordResult(3,
                                                                                                "UserRegistrationResult_PasswordMismatch");

        ChangePasswordResult(int value, string resultResourceStringName) : base(value, resultResourceStringName)
        {
        }

        protected override ResourceManager Resource
        {
            get { return MembershipStrings.ResourceManager; }
        }
    }
}