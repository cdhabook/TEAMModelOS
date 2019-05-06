

using TEAMModelOS.SDK.Extension.DataResult.RequestData;

namespace TEAMModelOS.SDK.Extension.DataResult.JsonRpcRequest
{
    public  class PaginationJosnRPCRequest<T> : BaseJosnRPCRequest
    {
        public PaginationJosnRPCRequest()
        {
            @params = new PaginationRequest<T>();
        }
        public PaginationRequest<T> @params { get; set; }
    }
}
