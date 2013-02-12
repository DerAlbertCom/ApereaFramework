using System;
using System.Web.Mvc;
using StructureMap;

namespace Aperea.ActionFilter
{
    public class ReleaseAndDisposeAllHttpScopedObjectsFilter : IResultFilter
    {
        public void OnResultExecuting(ResultExecutingContext filterContext)
        {
            
        }

        public void OnResultExecuted(ResultExecutedContext filterContext)
        {
            ObjectFactory.ReleaseAndDisposeAllHttpScopedObjects();
        }
    }
}