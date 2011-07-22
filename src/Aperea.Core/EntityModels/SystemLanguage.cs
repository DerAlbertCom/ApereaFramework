using System.ComponentModel.DataAnnotations;
using System.Globalization;
using Newtonsoft.Json;

namespace Aperea.EntityModels
{
    public class SystemLanguage
    {
        CultureInfo _cultureInfo;

        protected SystemLanguage()
        {
        }

        public SystemLanguage(string culture)
        {
            Culture = culture;
        }

        public string Id { get; set; }

        string _culture;

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

        [JsonIgnore]
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