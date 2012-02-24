using System;
using System.Linq;
using System.Reflection;

namespace Aperea.Extensions
{
    public static class TypeExtensions
    {
        public static MethodInfo GetGenericMethod(this Type type, string name, params Type[] types)
        {
            var methodInfo = (from mi in type.GetMethods()
                              where
                                  mi.Name == name &&
                                  mi.IsGenericMethod &&
                                  mi.GetGenericArguments().Length == types.Length
                              select mi).First();

            return methodInfo.MakeGenericMethod(types);
        }
    }
}