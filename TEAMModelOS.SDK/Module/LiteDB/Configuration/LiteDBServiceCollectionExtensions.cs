using TEAMModelOS.SDK.Module.LiteDB.Implements;
using TEAMModelOS.SDK.Module.LiteDB.Interfaces;
using Microsoft.Extensions.DependencyInjection;


namespace TEAMModelOS.SDK.Module.LiteDB.Configuration
{
    /// <summary>
    /// 
    /// </summary>
    public static class LiteDBServiceCollectionExtensions
    {
        public static LiteDBServiceBuilder Builder { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        private static LiteDBServiceBuilder AddServerBuilder(this IServiceCollection services)
        {
            return new LiteDBServiceBuilder(services);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static LiteDBServiceBuilder AddLiteDB(this IServiceCollection services)
        {
            if (Builder == null)
            {
                Builder = services.AddServerBuilder();
            }
            services.AddSingleton<ILiteDBOperator, LiteDBOperator>();
            return Builder;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static LiteDBServiceBuilder AddConnection(this LiteDBServiceBuilder builder, LiteDBOptions options)
        {
            builder.Services.AddSingleton(options);
            return builder;
        }
    }
}
