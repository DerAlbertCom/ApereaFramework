using System.Resources;
using Aperea.MVC.Annotations;

namespace Aperea.MVC.Authentication.Annotations
{
    internal class LabelNameAttribute : BaseLabelNameAttribute
    {
        public LabelNameAttribute(string resourceName) : base(resourceName)
        {
        }

        protected override ResourceManager GetResourceManager()
        {
            return MvcResourceStrings.ResourceManager;
        }
    }
}