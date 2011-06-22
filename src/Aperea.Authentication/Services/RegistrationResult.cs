using System;
using System.Resources;

namespace Aperea.Services
{
    public class RegistrationResult : Enumeration<int>
    {
        RegistrationResult(int value, string resultResourceStringName)
            : base(value, resultResourceStringName)
        {
        }

        public static readonly RegistrationResult Ok = new RegistrationResult(0, "RegistrationResult_Ok");

        public static readonly RegistrationResult Exists = new RegistrationResult(1, "Warning_Login_Exists");

        public static readonly RegistrationResult InvalidLoginData = new RegistrationResult(2,
                                                                                            "Warning_Login_DataInvalid");

        public static readonly RegistrationResult PasswordMismatch = new RegistrationResult(3,
                                                                                            "Warning_Login_PasswordMismatch");

        protected override ResourceManager Resource
        {
            get { return MembershipStrings.ResourceManager; }
        }
    }
}