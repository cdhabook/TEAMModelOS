using TEAMModelOS.SDK.Extension.DataResult.PageToken;
using MessagePack;

namespace TEAMModelOS.SDK.Extension.DataResult.JsonRpcResponse
{
    [MessagePackObject(keyAsPropertyName: true)]
    public class PageJsonRPCResult<T> : JsonRPCResult<T>
    {
        public Pagination page { get; set; }

        public PageJsonRPCResult()
        {
        }
    }
}
