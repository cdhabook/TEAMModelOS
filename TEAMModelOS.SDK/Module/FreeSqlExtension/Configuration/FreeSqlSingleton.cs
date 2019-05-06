using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace TEAMModelOS.SDK.Module.FreeSqlExtension.Configuration
{
    public sealed class FreeSqlSingleton
    {
        private static FreeSqlConnectionConfig _connectionConfig;
        private static ILoggerFactory _loggerFactory;
        private IFreeSql freeSql;
        private FreeSqlSingleton() { }
        public IFreeSql GetTableClient()
        {
            if (freeSql != null)
            {
                return freeSql;
            }
            else
            {
                getInstance(_connectionConfig , _loggerFactory);
                return freeSql;
            }
        }
        public static FreeSqlSingleton getInstance(FreeSqlConnectionConfig connectionConfig, ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory;
            _connectionConfig = connectionConfig;
            return SingletonInstance.instance;
        }
        private static class SingletonInstance
        {
            public static FreeSqlSingleton instance = new FreeSqlSingleton()
            {
                freeSql = new FreeSql.FreeSqlBuilder()
                            .UseConnectionString(_connectionConfig.DataType, _connectionConfig.ConnectionString)
                            .UseLogger(_loggerFactory.CreateLogger<IFreeSql>())
                            .UseAutoSyncStructure(_connectionConfig.AutoSyncStructure) //自动迁移实体的结构到数据库
                            .Build()
            };
        }
    }
}
