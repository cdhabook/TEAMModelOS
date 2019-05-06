using MessagePack;
using System;


namespace TEAMModelOS.SDK.Extension.DataResult.ResponseData
{
    [MessagePackObject(keyAsPropertyName: true)]
    public class TimeStampResponse
    {
        public long responseTime { get; set; } = DateTime.Now.ToUniversalTime().Ticks - 621355968000000000;
        public TimeStampResponse()
        {

        }
    }
}
