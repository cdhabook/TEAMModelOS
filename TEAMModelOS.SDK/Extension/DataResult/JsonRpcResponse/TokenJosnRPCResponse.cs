using MessagePack;
using System;
using System.Collections.Generic;
using System.Text;

namespace TEAMModelOS.SDK.Extension.DataResult.JsonRpcResponse
{
    [MessagePackObject(keyAsPropertyName: true)]
    public class TokenJosnRPCResponse<T>: BaseJosnRPCResponse
    {
		public TokenJosnRPCResponse()
		{
			result = new TokenJsonRPCResult<T>();
		}
		public  TokenJsonRPCResult<T> result { get; set; }
    }
}
