using MessagePack;
using System;
using System.Collections.Generic;
using System.Text;

namespace TEAMModelOS.SDK.Extension.DataResult.JsonRpcResponse
{
    [MessagePackObject(keyAsPropertyName: true)]
    public class DataJosnRPCResponse<T> : BaseJosnRPCResponse
    {
		public DataJosnRPCResponse() { 
		  result=  new JsonRPCResult<T>();
		}
		public   JsonRPCResult<T> result { get; set; } 
    }
}
