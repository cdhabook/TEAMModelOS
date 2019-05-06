using MessagePack;
using Microsoft.VisualBasic;

namespace TEAMModelOS.SDK.Extension.DataResult.JsonRpcResponse
{
    [MessagePackObject(keyAsPropertyName: true)]
    public class BaseJosnRPCResponse 
    {
        public string jsonrpc { get; set; } = "2.0";
        public double id { get; set; } = 1;
        private object result { get; set; }
        public object error { get; set; } = null;
    }
}
