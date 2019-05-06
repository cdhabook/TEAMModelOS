using MessagePack;
using System;
using System.Collections.Generic;
using System.Text;

namespace TEAMModelOS.SDK.Extension.DataResult.JsonRpcResponse
{
    [MessagePackObject(keyAsPropertyName: true)]
    public class JsonRPCResult<T>
    {
        public Dictionary<string, object> extend { get; set; } = null;
        public long responseTime { get; set; } = DateTime.Now.ToUniversalTime().Ticks - 621355968000000000;
        public T data { get; set; }
        public string message { get; set; } = "";
    }
}
