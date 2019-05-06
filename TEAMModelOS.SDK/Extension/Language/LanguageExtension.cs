using TEAMModelOS.SDK.Extension.Language.Implements;
using TEAMModelOS.SDK.Extension.Language.Interfaces;
using TEAMModelOS.SDK.Extension.Language.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace TEAMModelOS.SDK.Extension.Language
{
    public static class RedisExtension
    {
        public static void AddLanguage(this IServiceCollection services, IConfigurationSection LangConfiguration)
        {
            services.Configure<List<SmsCountryCode>>(LangConfiguration);
            services.AddSingleton<ILanguageService, LanguageService>();
        }
    }
}
