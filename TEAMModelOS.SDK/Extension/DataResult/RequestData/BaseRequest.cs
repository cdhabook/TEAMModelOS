using System;
using System.Collections.Generic;

namespace TEAMModelOS.SDK.Extension.DataResult.RequestData
{
    public class BaseRequest
    {
        public long requestTime { get; set; } = DateTime.Now.ToUniversalTime().Ticks - 621355968000000000;
        public string method { get; set; }
        public string repeatToken { get; set; }
    }
}
