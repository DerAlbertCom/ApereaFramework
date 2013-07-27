using System;

namespace Aperea.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class DontValidateAntiforgeryTokenAttribute : Attribute
    {
    }
}