using TEAMModelOS.SDK.Module.AzureTable.Implements;
using TEAMModelOS.SDK.Module.AzureTable.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace TEAMModelOS.SDK.Module.AzureTable.Configuration
{
    public static class AzureTableServiceCollectionExtensions
    {
        public static AzureTableServiceBuilder Builder { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        private static AzureTableServiceBuilder AddServerBuilder(this IServiceCollection services)
        {
            return new AzureTableServiceBuilder(services);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static AzureTableServiceBuilder AddAzureTableStorage(this IServiceCollection services)
        {
            if (Builder == null)
            {
                Builder = services.AddServerBuilder();
            }
            //services.AddSingleton<IAzureTableDBRepository, AzureTableDBRepository>();
            return Builder;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="_connectionString"></param>
        /// <returns></returns>
        public static AzureTableServiceBuilder AddConnection(this AzureTableServiceBuilder builder, AzureTableOptions databaseOptions)
        {
            //builder.Services.AddSingleton(databaseOptions);
            return builder;
        }
        
    }
}
