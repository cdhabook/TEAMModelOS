using Microsoft.Extensions.DependencyInjection;
using System;

namespace TEAMModelOS.SDK.Module.AzureTable.Configuration
{
    public class AzureTableServiceBuilder : ServiceCollection
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        public AzureTableServiceBuilder(IServiceCollection services)
        {
            Services = services ?? throw new ArgumentNullException(nameof(services));
        }

        /// <summary>
        /// 
        /// </summary>
        public IServiceCollection Services { get; }
    }
}
