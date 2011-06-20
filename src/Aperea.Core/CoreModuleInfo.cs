namespace Aperea
{
    public class CoreModuleInfo : IModuleInfo
    {
        public string Name
        {
            get { return "Aperea.Core";  }
        }

        public string Description
        {
            get { return "The CoreFramwork"; }
        }

        public int Version
        {
            get { return 1; }
        }

        public string Website
        {
            get { return "http://der-albert.com"; }
        }
    }
}