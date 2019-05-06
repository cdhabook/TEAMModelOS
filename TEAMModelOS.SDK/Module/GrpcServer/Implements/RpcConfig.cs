using GrpcServer = Grpc.Core;
using Grpc.Core;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Linq;
using System.Net.Sockets;
using TEAMModelOS.SDK.Module.GrpcServer.Interfaces;
using TEAMModelOS.SDK.Module.GrpcServer.Extensions;
using Grpc.HealthCheck;
using Grpc.Health.V1;
using TEAMModelOS.SDK.Module.GrpcServer.Internal;
using TEAMModelOS.SDK.Helper.Network.NetworkHelper;

namespace TEAMModelOS.SDK.Module.GrpcServer.Implements
{
    public class RpcConfig : IRpcConfig
    {
        /// <summary>
        /// 注入外部Proto gRPC Service
        /// service.Add(MsgService.BindService(new MsgServiceImpl()));
        /// </summary>
        private static List<Server> _servers { get; set; }
        List<IGrpcService> _grpcServices;
        private static List<string> _iPConfig { get; set; }
        public static IList<Dictionary<string, object>> ListInfo { get; } = new List<Dictionary<string, object>>();
        static GrpcServerConfig[] _serverConfigs;

        public RpcConfig(GrpcServerConfig[] serverConfigs  , GrpcIPConfig iPConfig , IEnumerable<IGrpcService> grpcServices)
        {
            _grpcServices = grpcServices.ToList();
            _iPConfig = iPConfig.BlackList;
            _serverConfigs = serverConfigs;
            _servers = new List<Server>();
        }

        public void Start()
        {
            
            HealthServiceImpl healthServiceImpl = new HealthServiceImpl();
            healthServiceImpl.SetStatus("HealthCheckService", HealthCheckResponse.Types.ServingStatus.Serving);
            List<Cnode> services = new List<Cnode>();
            List<string> ips = NetHelper.getIPAddress();
            ips = ips.Where(a => !_iPConfig.Exists(t => a.Contains(t))).ToList();
            var builder = ServerServiceDefinition.CreateBuilder();
            _grpcServices.ForEach(grpc => grpc.RegisterMethod(builder));
            foreach (GrpcServerConfig config in _serverConfigs) {
                foreach (string ip in ips) {
                    //获取扫描的_grpcServices
                  
                    _grpcServices.ForEach(grpc => services.Add(new Cnode { Name = grpc.GetType().FullName, Ip = ip, Port = config.Port }));
                    Server  _server = new Server
                    {
                        Services = { builder.Build() },
                        Ports = { new ServerPort(ip, config.Port, ServerCredentials.Insecure) }
                    };
                    //获取健康检查
                    _server.Services.Add(Health.BindService(healthServiceImpl));
                    services.Add(new Cnode { Port = config.Port, Ip = ip, Name = Health.Descriptor.FullName });
                    //获取手动注入的
                    foreach (GrpcServerServiceDefinition definition in config.GrpcServices)
                    {
                        _server.Services.Add(definition.ServerServiceDefinition);
                        Dictionary<string, object> service = new Dictionary<string, object>();
                        Cnode cnode = new Cnode { Port=config.Port, Ip=ip, Name=definition.ServiceDescriptor.FullName};
                        services.Add(cnode);
                    }
                    _server.Start();
                    _servers.Add(_server);
                }
            }
             ProtoGenerator.Gen("proto");
           
            foreach (IGrouping<string, Cnode> group in services.GroupBy(c =>c.Name)){
                Dictionary<string, object> dictInfo = new Dictionary<string, object>();
                List<Node> nodes = new List<Node>();
                foreach(Cnode dict in group) {
                    Node node = new Node { Ip =dict.Ip,Port=dict.Port };
                    nodes.Add(node);
                }
                dictInfo.Add("name", group.Key);
                dictInfo.Add("node", nodes);
                ListInfo.Add(dictInfo);
            }
        }
    }

    internal class Cnode
    {
        public string Ip { get; set; }
        public int Port { get; set; }
        public string Name { get; set; }
    }

    internal class Node {
        public string Ip { get; set; }
        public int Port { get; set; }
    }
}
