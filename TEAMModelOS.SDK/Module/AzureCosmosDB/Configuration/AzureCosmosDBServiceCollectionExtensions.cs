using TEAMModelOS.SDK.Module.AzureCosmosDB.Implements;
using TEAMModelOS.SDK.Module.AzureCosmosDB.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace TEAMModelOS.SDK.Module.AzureCosmosDB.Configuration
{
    public static class AzureCosmosDBServiceCollectionExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        private static AzureCosmosDBServiceBuilder AddCosmosDBServerBuilder(this IServiceCollection services)
        {
            return new AzureCosmosDBServiceBuilder(services);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static AzureCosmosDBServiceBuilder AddAzureCosmosDB(this IServiceCollection services)
        {
            var builder = services.AddCosmosDBServerBuilder();
            services.AddSingleton<IAzureCosmosDBRepository, AzureCosmosDBRepository>();
            return builder;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="_connectionString"></param>
        /// <returns></returns>
        public static AzureCosmosDBServiceBuilder AddCosmosDBConnection(this AzureCosmosDBServiceBuilder builder, AzureCosmosDBOptions databaseOptions)
        {
            builder.Services.AddSingleton(databaseOptions);
            return builder;
        }
    }
}
