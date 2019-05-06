using TEAMModelOS.SDK.Extension.DataResult.PageToken;
using MessagePack;
using System;
using System.Collections.Generic;
using System.Text;

namespace TEAMModelOS.SDK.Extension.DataResult.JsonRpcResponse
{
    [MessagePackObject(keyAsPropertyName: true)]
    public class TokenJsonRPCResult<T> : JsonRPCResult<T>
    {
       
        public AzureTableToken azureToken { get; set; }
        public TokenJsonRPCResult() { }
    }
}
