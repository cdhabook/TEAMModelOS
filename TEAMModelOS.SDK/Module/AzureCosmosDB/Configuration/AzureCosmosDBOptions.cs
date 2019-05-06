using System;
using System.Collections.Generic;
using System.Text;

namespace TEAMModelOS.SDK.Module.AzureCosmosDB.Configuration
{
    public class AzureCosmosDBOptions
    {
        public string ConnectionString { get; set; } = null;
        public string ConnectionKey { get; set; } = null;
        public string Database { get; set; } = null;
        public string AzureTableDialect { get; set; } = null;
        public int CollectionThroughput { get; set; } = 400;
        
        public AzureCosmosDBOptions()
        {
            //Azure Table Init
        }
    }
}
