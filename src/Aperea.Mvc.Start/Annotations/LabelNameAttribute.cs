using System.ComponentModel;

namespace ApereaStart.Annotations
{
    public class LabelNameAttribute : DisplayNameAttribute
    {
        readonly string _resourceName;

        public LabelNameAttribute(string resourceName)
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
            string displayName = ResourceStrings.ResourceManager.GetString(_resourceName,
                                                                   System.Threading.Thread.CurrentThread.
                                                                       CurrentUICulture);
            if (string.IsNullOrEmpty(displayName)) {
                displayName = "Missing: " + _resourceName;
            }
            return displayName;
        }
    }
}