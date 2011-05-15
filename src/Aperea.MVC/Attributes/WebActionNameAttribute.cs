using System;

namespace Aperea.MVC.Attributes
{
    public class WebActionNameAttribute : Attribute
    {
        readonly string _webActionName;

        public string WebActionName
        {
            get { return _webActionName; }
        }

        public WebActionNameAttribute(string webActionName)
        {
            _webActionName = webActionName;
        }
    }
}