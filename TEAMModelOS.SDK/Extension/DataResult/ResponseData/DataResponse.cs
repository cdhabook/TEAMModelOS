using MessagePack;

namespace TEAMModelOS.SDK.Extension.DataResult.ResponseData
{
    [MessagePackObject(keyAsPropertyName: true)]
    public class DataResponse<T> : BaseResponse
    {
        public T data { get; set; }

        public DataResponse()
        {
        }
    }
}
