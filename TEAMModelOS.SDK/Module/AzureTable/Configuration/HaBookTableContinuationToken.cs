using MessagePack;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.ComponentModel.DataAnnotations;
using TEAMModelOS.SDK.Extension.DataResult.PageToken;
using TEAMModelOS.SDK.Helper.Common.ValidateHelper;

namespace TEAMModelOS.SDK.Module.AzureTable.Configuration
{
    [MessagePackObject(keyAsPropertyName: true)]
    public class HaBookTableContinuationToken
    {
        [Required]
        public string NextPartitionKey { get; set; }
        [Required]
        public string NextRowKey { get; set; }
        [Required]
        public string NextTableName { get; set; }
        [Required]
        public StorageLocation? TargetLocation { get; set; }
        public TableContinuationToken GetContinuationToken() {
            TableContinuationToken continuationToken = new TableContinuationToken
            {
                NextPartitionKey = this.NextPartitionKey,
                NextRowKey = this.NextRowKey,
                NextTableName = this.NextTableName,
                TargetLocation = this.TargetLocation
            };
            return ValidateHelper.ValidObj(continuationToken);
        }
        public HaBookTableContinuationToken() {
        }
        public HaBookTableContinuationToken(TableContinuationToken continuationToken) {
            if (null != continuationToken) {
                this.NextPartitionKey = continuationToken.NextPartitionKey;
                this.NextRowKey = continuationToken.NextRowKey;
                this.NextTableName = continuationToken.NextTableName;
                this.TargetLocation = continuationToken.TargetLocation;
            }
        }

        public HaBookTableContinuationToken(AzureTableToken continuationToken)
        {
            if (null != continuationToken) {
                this.NextPartitionKey = continuationToken.NextPartitionKey;
                this.NextRowKey = continuationToken.NextRowKey;
                this.NextTableName = continuationToken.NextTableName;
                int index = 0;
                foreach (StorageLocation item in Enum.GetValues(typeof(StorageLocation)))
                {
                    if (continuationToken.TargetLocation == index)
                    {
                        this.TargetLocation = item;
                        break;
                    }
                    index++;
                }
            }
        }
        public AzureTableToken GetAzureTableToken() {
            AzureTableToken continuationToken = new AzureTableToken
            {
                NextPartitionKey = this.NextPartitionKey,
                NextRowKey = this.NextRowKey,
                NextTableName = this.NextTableName
            };
            ///枚举遍历
            ///
            int index = 0;
            foreach (StorageLocation item in Enum.GetValues(typeof(StorageLocation))) {
                if (this.TargetLocation == item) {
                    continuationToken.TargetLocation = index;
                    break;
                }
                index++;
            }
            return ValidateHelper.ValidObj(continuationToken); 
        }
    }
}
