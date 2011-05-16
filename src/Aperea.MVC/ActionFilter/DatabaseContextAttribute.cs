using System;
using System.Web.Mvc;
using Aperea.Infrastructure.Data;
using Aperea.MVC.StateProvider;

namespace Aperea.MVC.ActionFilter
{
    public class DatabaseContextAttribute : DependencyFilterAttribute, IActionFilter
    {
        readonly IStateStorage _state = new HttpContextStateStorage();

        public DatabaseContextAttribute()
        {
            Order = -1;
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            _state.Set("DatabaseContextAttribute.Context", GetService<IDatabaseContext>());
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            _state.Get<IDatabaseContext>("DatabaseContextAttribute.Context").Dispose();
        }
    }
}