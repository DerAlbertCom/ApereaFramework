using System;
using System.Resources;

namespace Aperea.Services
{
    public class UserRegistrationResult : Enumeration<int>
    {
        private UserRegistrationResult(int value, string resultResourceStringName)
            : base(value, resultResourceStringName)
        {
        }

        public static readonly UserRegistrationResult Ok = new UserRegistrationResult(0, "UserRegistrationResult_Ok");

        public static readonly UserRegistrationResult Exists = new UserRegistrationResult(1,
                                                                                          "UserRegistrationResult_Exists");

        public static readonly UserRegistrationResult InvalidUserdata = new UserRegistrationResult(2,
                                                                                                   "UserRegistrationResult_InvalidUserdata");

        public static readonly UserRegistrationResult PasswordMismatch = new UserRegistrationResult(3,
                                                                                                    "UserRegistrationResult_PasswordMismatch");

        protected override ResourceManager Resource
        {
            get { return MembershipStrings.ResourceManager; }
        }
    }
}