using Google.Protobuf.Reflection;
using Grpc.Core;
using Microsoft.Extensions.DependencyInjection;
using ServiceDescriptor = Google.Protobuf.Reflection.ServiceDescriptor;

namespace TEAMModelOS.SDK.Module.GrpcServer.Extensions
{
    public class GrpcServerServiceDefinition 
    {
        public ServiceDescriptor ServiceDescriptor { get; set; }

        public ServerServiceDefinition ServerServiceDefinition { get;  set; }
    }
}
