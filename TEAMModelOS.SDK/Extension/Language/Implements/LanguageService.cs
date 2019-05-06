using TEAMModelOS.SDK.Extension.Language.Interfaces;
using TEAMModelOS.SDK.Extension.Language.Model;
using Microsoft.Extensions.Options;
using System.Collections.Generic;

namespace TEAMModelOS.SDK.Extension.Language.Implements
{
    public class LanguageService : ILanguageService
    {
        private Dictionary<string, SmsCountryCode> smsMap { get; set; }
        public List<SmsCountryCode> countryCodes;

        public LanguageService(IOptions<List<SmsCountryCode>> _option)
        {
            countryCodes = _option.Value;
           
        }
        private LanguageService SmsLanguage()
        {
            foreach (SmsCountryCode sms in countryCodes) {
                if (this.smsMap == null)
                {
                    smsMap = new Dictionary<string, SmsCountryCode>();
                }
                if (!smsMap.ContainsKey(sms.CountryCode))
                {
                    smsMap.Add(sms.CountryCode, sms);
                }
            }
            return this;
        }
        public Dictionary<string, SmsCountryCode> GetSmsLanguage()
        {
            SmsLanguage();
            return smsMap;
        }
    }
}
