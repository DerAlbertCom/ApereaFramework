using System;

namespace Aperea.MVC.Attributes
{
    public class RemoteActionNameAttribute : Attribute
    {
        readonly string _webActionName;

        public string WebActionName
        {
            get { return _webActionName; }
        }

        public RemoteActionNameAttribute(string webActionName)
        {
            _webActionName = webActionName;
        }
    }
}