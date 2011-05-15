using System;
using System.Web.Mvc;

namespace Aperea.MVC.Extensions
{
    public static class TempDataDictionaryExtensions
    {
        public static void SetModel<T>(this TempDataDictionary tempData, T model)
        {
            tempData["Model"] = model;
        }

        public static T GetModel<T>(this TempDataDictionary tempData) where T : class
        {
            var t = tempData["Model"] as T;
            return t ?? Activator.CreateInstance<T>();
        }
    }
}