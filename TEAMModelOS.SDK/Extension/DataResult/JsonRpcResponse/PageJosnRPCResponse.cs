using MessagePack;
using System;
using System.Collections.Generic;
using System.Text;

namespace TEAMModelOS.SDK.Extension.DataResult.JsonRpcResponse
{
    [MessagePackObject(keyAsPropertyName: true)]
    public class PageJosnRPCResponse<T> : BaseJosnRPCResponse
    {
		public PageJosnRPCResponse()
		{
			result = new PageJsonRPCResult<T>();
		}
		public   PageJsonRPCResult<T> result { get; set; }
    }
}
