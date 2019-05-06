using TEAMModelOS.SDK.Module.AzureBlob.Implements;
using TEAMModelOS.SDK.Module.AzureBlob.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace TEAMModelOS.SDK.Module.AzureBlob.Configuration
{
    public static class AzureBlobServiceCollectionExtensions
    {
        public static AzureBlobServiceBuilder Builder { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        private static AzureBlobServiceBuilder AddServerBuilder(this IServiceCollection services)
        {
            return new AzureBlobServiceBuilder(services);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static AzureBlobServiceBuilder AddAzureBlobStorage(this IServiceCollection services)
        {
            if (Builder == null)
            {
                Builder = services.AddServerBuilder();
            }
            services.AddSingleton<IAzureBlobDBRepository, AzureBlobDBRepository>();
            return Builder;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="_connectionString"></param>
        /// <returns></returns>
        public static AzureBlobServiceBuilder AddConnection(this AzureBlobServiceBuilder builder, AzureBlobOptions databaseOptions)
        {
            builder.Services.AddSingleton(databaseOptions);
            return builder;
        }
    }
}
