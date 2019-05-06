using Grpc.Core;
using TEAMModelOS.SDK.Module.GrpcServer.Implements;
using TEAMModelOS.SDK.Module.GrpcServer.Interfaces;
using TEAMModelOS.SDK.Module.GrpcServer.Internal;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TEAMModelOS.SDK.Module.GrpcServer.Extensions
{

    /// <summary>
    /// Grpc 配置相关的扩展方法
    /// </summary>
    public static class GrpcServerConfigurationExtensions
    {
        public static GrpcServerServiceBuilder Builder { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        private static GrpcServerServiceBuilder AddServerBuilder(this IServiceCollection services)
        {
            return new GrpcServerServiceBuilder(services);
        }

        /// <summary>
        /// 注册 Grpc 服务
        /// </summary>
        /// <param name="configs"> 配置模块</param>
        /// <param name="optionAction">配置操作</param>
        public static GrpcServerServiceBuilder AddGrpcServer(this IServiceCollection services)
        {
            if (Builder == null)
            {
                Builder = services.AddServerBuilder();
            }
            //自动扫描基于 IGrpcService 的Grpc服务
            //services.Scan(scan => scan.FromAssemblyOf<IGrpcService>()
            //   .AddClasses().AsImplementedInterfaces().WithSingletonLifetime());
            //引入当前
            services.Scan(scan => scan.FromApplicationDependencies()
               .AddClasses(classes => classes.AssignableTo<IGrpcService>())
                   .AsImplementedInterfaces()
                   .WithSingletonLifetime());
            //services.Scan(scan => scan.FromApplicationDependencies()
            //   .AddClasses().AsImplementedInterfaces().WithSingletonLifetime());
            services.AddSingleton<IRpcConfig, RpcConfig>();
            return Builder;
        }
        /// <summary>
        /// Grpc配置信息
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static GrpcServerServiceBuilder AddGrpcConfiguration(this GrpcServerServiceBuilder builder, GrpcServerConfig[] serverConfigs)
        {
            builder.Services.AddSingleton(serverConfigs);
            return builder;
        }
        /// <summary>
        /// Grpc 服务端IP 配置
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static GrpcServerServiceBuilder AddGrpcIpConfig(this GrpcServerServiceBuilder builder,GrpcIPConfig config)
        {
            builder.Services.AddSingleton(config);
            return builder;
        }

        /// <summary>
        /// 注入基本配制
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public static GrpcServerServiceBuilder UseOptions(this GrpcServerServiceBuilder builder,  Action<GrpcExtensionsOptions> action)
        {
            action(GrpcExtensionsOptions.Instance);
            return builder;
        }

        /// <summary>
        /// CodeFirst生成proto文件
        /// </summary>
        /// <param name="dir"></param>
        /// <returns></returns>
        public static GrpcServerServiceBuilder UseProtoGenerate(this GrpcServerServiceBuilder builder,  string dir)
        {
            ProtoGenerator.Gen(dir);
            return builder;
        }

        /// <summary>
        /// 注入GrpcService
        /// </summary>
        /// <param name="grpcServices"></param>
        /// <returns></returns>
        public static GrpcServerServiceBuilder UseGrpcService(this GrpcServerServiceBuilder builder,  IEnumerable<IGrpcService> grpcServices)
        {
            var builders = ServerServiceDefinition.CreateBuilder();
            grpcServices.ToList().ForEach(grpc => grpc.RegisterMethod(builders));
          //  _serviceDefinitions.Add(builders.Build());
            return builder;
        }
    }
}
