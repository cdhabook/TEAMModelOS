using Grpc.Core;
using System.Collections.Generic;

namespace TEAMModelOS.SDK.Module.GrpcServer.Extensions
{
    /// <inheritdoc />
    public class GrpcServerConfig
    {
        /// <summary>
        /// 添加包含 Grpc 服务定义的程序集
        /// </summary>
        /// <param name="ServerServiceDefinition">服务程序集</param>
        public List<GrpcServerServiceDefinition> GrpcServices { get; set; }
       
        /// <inheritdoc />
        public GrpcServerConfig()
        {
            GrpcServices = new List<GrpcServerServiceDefinition>();
        }
        /// <inheritdoc />
        public string ServiceName { get; set; }

        /// <summary>
        /// Grpc 服务端绑定的监听地址
        /// </summary>
        public List<string> Ip { get; set; }

        /// <summary>
        /// Grpc 服务端绑定的监听端口
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// 存在 Grpc 服务的程序集集合
        /// </summary>
        //public IReadOnlyList<ServerServiceDefinition> GrpcAssemblies => grpcServices;
       
        //public void AddRpcServiceAssembly(ServerServiceDefinition serviceAssembly)
        //{
        //    grpcServices.Add(serviceAssembly);
        //}
    }
}
