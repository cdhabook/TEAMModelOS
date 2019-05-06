using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace TEAMModelOS.SDK.Module.AzureBlob.Configuration
{
     public  class AzureBlobServiceBuilder : ServiceCollection
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        public AzureBlobServiceBuilder(IServiceCollection services)
        {
            Services = services ?? throw new ArgumentNullException(nameof(services));
        }

        /// <summary>
        /// 
        /// </summary>
        public IServiceCollection Services { get; }
    }
}
