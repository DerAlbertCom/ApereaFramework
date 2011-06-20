using System.Web.Mvc;

namespace Aperea.MVC.ActionFilter
{
    public abstract class DependencyFilterAttribute : FilterAttribute
    {
        protected T GetService<T>()
        {
            return DependencyResolver.Current.GetService<T>();
        }
    }
}