using System;
using System.Collections.Generic;
using System.Text;

namespace TEAMModelOS.SDK.Module.AzureTable.Configuration
{
    public class AzureTableOptions
    {
        public string ConnectionString { get; set; } 
        public string AzureTableDialect { get; set; } 
        public string DialectKey { get; set; } 
        /// <summary>
        /// 
        /// </summary>
        public AzureTableOptions()
        {
        }
    }
}
