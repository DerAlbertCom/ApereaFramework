using System;
using System.Web.Mvc;
using Aperea.Infrastructure.IoC;
using StructureMap;
using StructureMap.Web.Pipeline;

namespace Aperea.ActionFilter
{
    public class ReleaseAndDisposeAllHttpScopedObjectsFilter : IResultFilter
    {
        public void OnResultExecuting(ResultExecutingContext filterContext)
        {
            
        }

        public void OnResultExecuted(ResultExecutedContext filterContext)
        {
            HttpContextLifecycle.DisposeAndClearAll();
        }
    }
}