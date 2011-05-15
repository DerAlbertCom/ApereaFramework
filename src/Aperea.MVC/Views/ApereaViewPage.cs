using System.Web.Mvc;

// based on a solution from Scott Hanselman
// http://www.hanselman.com/blog/ABetterASPNETMVCMobileDeviceCapabilitiesViewEngine.aspx

namespace Aperea.MVC.Views
{
    public abstract class ApereaViewPage : WebViewPage
    {
        public override System.Web.WebPages.HelperResult RenderPage(string path, params object[] data)
        {
            return base.RenderPage(Context.GetLayoutPath(path), data);
        }
    }

    public abstract class ApereaViewPage<TModel> : WebViewPage<TModel>
    {
        public override System.Web.WebPages.HelperResult RenderPage(string path, params object[] data)
        {
            return base.RenderPage(Context.GetLayoutPath(path), data);
        }
    }
}