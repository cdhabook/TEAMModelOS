using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace TEAMModelOS.SDK.Module.LiteDB.Configuration
{
    public class LiteDBServiceBuilder : ServiceCollection
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        public LiteDBServiceBuilder(IServiceCollection services)
        {
            Services = services ?? throw new ArgumentNullException(nameof(services));
        }
        /// <summary>
        /// 
        /// </summary>
        public IServiceCollection Services { get; }
    }
}
