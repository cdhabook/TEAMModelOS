using System;
using System.Collections.Generic;
using System.Text;

namespace TEAMModelOS.SDK.Module.FreeSqlExtension.Configuration
{
    public class FreeSqlConnectionConfig
    {
        public string ConnectionString { get; set; }
        public FreeSql.DataType DataType { get; set; }

        public bool AutoSyncStructure { get; set; }
    }
}
