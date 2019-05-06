using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace TEAMModelOS.SDK.Module.GrpcServer.Extensions
{
    public  class GrpcServerServiceBuilder
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        public GrpcServerServiceBuilder(IServiceCollection services)
        {
            Services = services ?? throw new ArgumentNullException(nameof(services));
        }
        /// <summary>
        /// 
        /// </summary>
        public IServiceCollection Services { get; }
    }
}
