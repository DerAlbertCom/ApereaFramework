using System;
using System.Web;
using System.Web.Mvc;

// based on a solution from Scott Hanselman
// http://www.hanselman.com/blog/ABetterASPNETMVCMobileDeviceCapabilitiesViewEngine.aspx

namespace Aperea.MVC.Views
{
    public interface IAdaptiveViewEngine : IViewEngine
    {
        Func<HttpContextBase, bool> IsTheRightDevice { get; }
        string PathToSearch { get;}
    }
}