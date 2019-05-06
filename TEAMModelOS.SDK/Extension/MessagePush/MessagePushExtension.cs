using TEAMModelOS.SDK.Extension.MessagePush.Implements;
using TEAMModelOS.SDK.Extension.MessagePush.Interfaces;
using TEAMModelOS.SDK.Extension.MessagePush.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace TEAMModelOS.SDK.Extension.MessagePush
{
    public static class MessagePushExtension
    {
        public static void SendCloud(this IServiceCollection services, IConfigurationSection configuration)
        {
            services.Configure<SmsSendCloud>(configuration);
            services.AddSingleton<ISendCloudService, SendCloudService>();
        }
    }
}
