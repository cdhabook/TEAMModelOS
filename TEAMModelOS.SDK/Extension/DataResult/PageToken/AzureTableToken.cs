using MessagePack;
using System.ComponentModel.DataAnnotations;

namespace TEAMModelOS.SDK.Extension.DataResult.PageToken
{
    [MessagePackObject(keyAsPropertyName: true)]
    public class AzureTableToken
    {
        //[Required]
        public string NextPartitionKey { get; set; }
        //[Required]
        public string NextRowKey { get; set; }
        public string NextTableName { get; set; }
        //[Required]
        public int? TargetLocation { get; set; }
    }
}
