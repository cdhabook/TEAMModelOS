using TEAMModelOS.SDK.Extension.DataResult.RequestData;
using System;
using System.Collections.Generic;
using System.Text;

namespace TEAMModelOS.SDK.Extension.DataResult.JsonRpcRequest
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class AzureTokenJsonRPCRequest<T> : BaseJosnRPCRequest
    {

        public AzureTokenJsonRPCRequest (){
            @params = new AzureTokenRequest<T>();
        }
        public AzureTokenRequest<T> @params { get; set; }
    }
}
