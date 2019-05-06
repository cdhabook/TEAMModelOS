using MessagePack;
using System;
using System.Collections.Generic;
using System.Text;

namespace TEAMModelOS.SDK.Extension.DataResult.JsonRpcRequest
{
    [MessagePackObject(keyAsPropertyName: true)]
    public class JosnRPCRequest<T>:BaseJosnRPCRequest
    {
        public T @params { get; set; }
    }
}
