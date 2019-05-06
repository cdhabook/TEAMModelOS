using TEAMModelOS.SDK.Extension.DataResult.PageToken;
using System;
using System.Collections.Generic;
using System.Text;

namespace TEAMModelOS.SDK.Extension.DataResult.RequestData
{
   public class AzureTokenRequest<T> :BaseRequest
   {
        public T data { get; set; }
        public AzureTableToken azureToken { get; set; }
   }
}
