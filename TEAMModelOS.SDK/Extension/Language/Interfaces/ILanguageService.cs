using TEAMModelOS.SDK.Extension.Language.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace TEAMModelOS.SDK.Extension.Language.Interfaces
{
    public interface ILanguageService
    {
        Dictionary<string, SmsCountryCode> GetSmsLanguage();
    }
}
