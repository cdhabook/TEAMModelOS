using TEAMModelOS.SDK.Extension.DataResult.PageToken;
using MessagePack;

namespace TEAMModelOS.SDK.Extension.DataResult.ResponseData
{
    [MessagePackObject(keyAsPropertyName: true)]
    public class PageDatasResponse<T> : DataResponse<T>
    {
        public Pagination page { get; set; }

        public PageDatasResponse()
        {
        }
    }
}
