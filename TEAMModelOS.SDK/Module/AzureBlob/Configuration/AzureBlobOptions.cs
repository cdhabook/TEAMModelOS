using System;
using System.Collections.Generic;
using System.Text;

namespace TEAMModelOS.SDK.Module.AzureBlob.Configuration
{
   public class AzureBlobOptions
    {
        public string ConnectionString { get; set; } 
        public string AzureTableDialect { get; set; }
        public string Container { get; set; }
        public string DialectKey { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public AzureBlobOptions()
        {
        }
    }
}
