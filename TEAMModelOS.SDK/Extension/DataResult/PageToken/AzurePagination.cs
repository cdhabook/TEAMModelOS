using MessagePack;
using System.Collections.Generic;

namespace TEAMModelOS.SDK.Extension.DataResult.PageToken
{
    [MessagePackObject(keyAsPropertyName: true)]
    public class AzurePagination<T>
    {
        public AzurePagination(){}
        public List<T> data { get; set; }
        public AzureTableToken token { get; set; }
    }
}
