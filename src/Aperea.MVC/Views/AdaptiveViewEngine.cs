using System;
using System.Web;
using System.Web.Mvc;

// based on a solution from Scott Hanselman
// http://www.hanselman.com/blog/ABetterASPNETMVCMobileDeviceCapabilitiesViewEngine.aspx

namespace Aperea.MVC.Views
{
    public class AdaptiveViewEngine : IAdaptiveViewEngine
    {
        IViewEngine BaseViewEngine { get; set; }
        public Func<HttpContextBase, bool> IsTheRightDevice { get; private set; }
        public string PathToSearch { get; private set; }

        public AdaptiveViewEngine(Func<HttpContextBase, bool> isTheRightDevice, string pathToSearch,
                                  IViewEngine baseViewEngine)
        {
            BaseViewEngine = baseViewEngine;
            IsTheRightDevice = isTheRightDevice;
            PathToSearch = pathToSearch;
        }

        public ViewEngineResult FindPartialView(ControllerContext context, string viewName, bool useCache)
        {
            if (IsTheRightDevice(context.HttpContext))
            {
                return BaseViewEngine.FindPartialView(context, PathToSearch + "/" + viewName, useCache);
            }
            return new ViewEngineResult(new string[] {}); //we found nothing and we pretend we looked nowhere
        }

        public ViewEngineResult FindView(ControllerContext context, string viewName, string masterName, bool useCache)
        {
            if (IsTheRightDevice(context.HttpContext))
            {
                return BaseViewEngine.FindView(context, PathToSearch + "/" + viewName, masterName, useCache);
            }
            return new ViewEngineResult(new string[] {}); //we found nothing and we pretend we looked nowhere
        }

        public void ReleaseView(ControllerContext controllerContext, IView view)
        {
            var disposable = view as IDisposable;
            if (disposable != null)
                disposable.Dispose();
        }
    }
}