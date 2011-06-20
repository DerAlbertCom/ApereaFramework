using System.ComponentModel;
using System.Resources;

namespace Aperea.MVC.Annotations
{
    public abstract class BaseLabelNameAttribute : DisplayNameAttribute
    {
        readonly string _resourceName;

        protected BaseLabelNameAttribute(string resourceName)
        {
            _resourceName = resourceName;
        }

        public override string DisplayName
        {
            get { return GetDisplayName(); }
        }

        public string ResourceName
        {
            get { return _resourceName; }
        }

        string GetDisplayName()
        {
            string displayName = GetResourceManager().GetString(_resourceName, System.Threading.Thread.CurrentThread.
                                                                                   CurrentUICulture);
            if (string.IsNullOrEmpty(displayName))
            {
                displayName = "Missing: " + _resourceName;
            }
            return displayName;
        }

        protected abstract ResourceManager GetResourceManager();
    }
}