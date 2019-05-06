using TEAMModelOS.SDK.Extension.DataResult.PageToken;
using MessagePack;


namespace TEAMModelOS.SDK.Extension.DataResult.ResponseData
{
    [MessagePackObject(keyAsPropertyName: true)]
    public class TokenPageDatasResponse<T> : DataResponse<T>
    {
        public AzureTableToken azureToken { get; set; }

        public TokenPageDatasResponse() { }
    }
}
