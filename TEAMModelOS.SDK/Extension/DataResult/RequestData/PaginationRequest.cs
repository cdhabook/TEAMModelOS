using TEAMModelOS.SDK.Extension.DataResult.PageToken;

namespace TEAMModelOS.SDK.Extension.DataResult.RequestData
{
    public class PaginationRequest<T> : BaseRequest
    {
        public T data { get; set; }
        public Pagination  page{ get; set; }
    }
}
