using Microsoft.Extensions.DependencyInjection;
using TEAMModelOS.SDK.Extension.HttpClient.Implements;

namespace TEAMModelOS.SDK.Extension.HttpClient
{
   public static class HttpClientExtension
    {
        public static void AddHttp(this IServiceCollection services)
        {
            //services.AddHttpClient();
            //services.AddSingleton<HttpClientService>();
            services.AddHttpClient<HttpClientSendCloud>();
            services.AddHttpClient<HttpClientUserInfo>();
            services.AddHttpClient<HttpClientSchool>();
        }
    }
}
