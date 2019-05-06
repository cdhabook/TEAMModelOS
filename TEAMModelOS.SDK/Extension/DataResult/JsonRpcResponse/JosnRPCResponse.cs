using MessagePack;
using System;
using System.Collections.Generic;
using System.Text;

namespace TEAMModelOS.SDK.Extension.DataResult.JsonRpcResponse
{
    [MessagePackObject(keyAsPropertyName: true)]
    public class JosnRPCResponse<T>:BaseJosnRPCResponse
    {
        public  T result { get; set; }
    }
}
