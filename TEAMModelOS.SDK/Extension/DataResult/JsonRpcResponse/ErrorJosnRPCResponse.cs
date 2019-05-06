using MessagePack;
using System;
using System.Collections.Generic;
using System.Text;

namespace TEAMModelOS.SDK.Extension.DataResult.JsonRpcResponse
{
    [MessagePackObject(keyAsPropertyName: true)]
    public class ErrorJosnRPCResponse<E> : BaseJosnRPCResponse
    {
        public ErrorJosnRPCResponse() {
            error = new ErrorModel<E>();
        }
        public new ErrorModel<E> error { get; set; }
    }
}
