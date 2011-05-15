using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Aperea.EntityModels
{
    public class SystemLanguage
    {
        private CultureInfo _cultureInfo;

        protected SystemLanguage()
        {
        }

        public SystemLanguage(string culture)
        {
            Culture = culture;
        }

        public int Id { get; set; }

        private string _culture;

        [StringLength(6)]
        [Required]
        public string Culture
        {
            get { return _culture; }
            set
            {
                _culture = value;
                _cultureInfo = null;
            }
        }

        public string DisplayName
        {
            get
            {
                if (_cultureInfo == null)
                {
                    _cultureInfo = CultureInfo.GetCultureInfo(_culture);
                }
                return _cultureInfo.NativeName;
            }
        }
    }
}