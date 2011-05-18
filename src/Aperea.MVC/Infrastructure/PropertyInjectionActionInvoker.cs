using System.Collections.Generic;
using System.Web.Mvc;
using StructureMap;

namespace Aperea.MVC.Infrastructure
{
    public class PropertyInjectionActionInvoker : ControllerActionInvoker
    {
        readonly IContainer _container;

        public PropertyInjectionActionInvoker()
        {
            _container = DependencyResolver.Current.GetService<IContainer>();
        }

        protected override ActionResult CreateActionResult(ControllerContext controllerContext,
                                                           ActionDescriptor actionDescriptor, object actionReturnValue)
        {
            var result = base.CreateActionResult(controllerContext, actionDescriptor, actionReturnValue);
            _container.BuildUp(result);
            return result;
        }
    }
}