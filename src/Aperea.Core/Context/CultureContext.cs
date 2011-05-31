using System.Threading;

namespace Aperea.Context
{
    public class CultureContext : ICultureContext
    {
        public string CurrentCulture
        {
            get { return Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName; }
        }
    }
}