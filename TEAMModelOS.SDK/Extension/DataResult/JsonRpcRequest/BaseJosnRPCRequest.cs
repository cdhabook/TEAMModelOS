using MessagePack;
using System;

namespace TEAMModelOS.SDK.Extension.DataResult.JsonRpcRequest
{
    [MessagePackObject(keyAsPropertyName: true)]
    public abstract class BaseJosnRPCRequest
    {
        public long requestTime { get; set; } = DateTime.Now.ToUniversalTime().Ticks - 621355968000000000;
        public string jsonrpc { get; set; } = "2.0";
        public string method { get; set; }
        public int id { get; set; } = 1;
        public int timeOffset { get; set; }
        public string lang { get; set; } = "zh-CN";
    }
}
