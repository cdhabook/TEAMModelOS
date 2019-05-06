using Grpc.Core;

namespace TEAMModelOS.SDK.Module.GrpcServer.Interfaces
{
    public interface IGrpcService
    {
        /// <summary>
        /// 注册服务方法
        /// </summary>
        void RegisterMethod(ServerServiceDefinition.Builder builder);
    }
    /// <summary>
    /// 基础服务
    /// </summary>
    public interface IGrpcBaseService : IGrpcService
    {

    }
}
