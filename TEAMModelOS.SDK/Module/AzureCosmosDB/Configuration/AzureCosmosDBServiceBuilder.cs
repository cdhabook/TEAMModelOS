using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace TEAMModelOS.SDK.Module.AzureCosmosDB.Configuration
{
    public class AzureCosmosDBServiceBuilder
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        public AzureCosmosDBServiceBuilder(IServiceCollection services)
        {
            Services = services ?? throw new ArgumentNullException(nameof(services));
        }

        /// <summary>
        /// 
        /// </summary>
        public IServiceCollection Services { get; }
    }
}
