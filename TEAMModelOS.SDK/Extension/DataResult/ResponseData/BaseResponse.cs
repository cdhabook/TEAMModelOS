using MessagePack;
using System.Collections.Generic;

namespace TEAMModelOS.SDK.Extension.DataResult.ResponseData
{
    [MessagePackObject(keyAsPropertyName: true)]
    public class BaseResponse : TimeStampResponse
    {
        public Dictionary<string, object> extend { get; set; }
        public string message { get; set; } = "success";
        public int code { get; set; }
        public BaseResponse()
        {
        }
    }
}
