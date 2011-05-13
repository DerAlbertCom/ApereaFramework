using System.Collections.Generic;
using System.Web.Mvc;
using StructureMap;

namespace Aperea.MVC.Infrastructure
{
    public class PropertyInjectionActionInvoker : ControllerActionInvoker
    {
        private readonly IContainer _container;

        public PropertyInjectionActionInvoker()
        {
            _container = DependencyResolver.Current.GetService<IContainer>();
        }

        protected override FilterInfo GetFilters(ControllerContext controllerContext, ActionDescriptor actionDescriptor)
        {
            var filters = base.GetFilters(controllerContext, actionDescriptor);
            BuildUp(filters.ActionFilters);
            BuildUp(filters.AuthorizationFilters);
            BuildUp(filters.ExceptionFilters);
            BuildUp(filters.ResultFilters);
            return filters;
        }

        protected override ActionResult CreateActionResult(ControllerContext controllerContext,
                                                           ActionDescriptor actionDescriptor, object actionReturnValue)
        {
            _container.BuildUp(actionReturnValue);
            return base.CreateActionResult(controllerContext, actionDescriptor, actionReturnValue);
        }

        private void BuildUp(IEnumerable<object> targets)
        {
            foreach (var target in targets){
                _container.BuildUp(target);
            }
        }
    }
}