using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace TEAMModelOS.SDK.Module.AzureTable.Configuration
{
    public sealed class TableClientSingleton
    {
        private static string _connectionString;
        private CloudTableClient TableClient;
        private TableClientSingleton() { }
        public CloudTableClient GetTableClient()
        {
            if (TableClient != null)
            {
                return TableClient;
            }
            else
            {
                getInstance(_connectionString);
                return TableClient;
            }
        }
        public static TableClientSingleton getInstance(string connectionString)
        {
            _connectionString = connectionString;
            return SingletonInstance.instance;
        }
        private static class SingletonInstance
        {
            public static TableClientSingleton instance = new TableClientSingleton()
            {
                TableClient = CloudStorageAccount.Parse(_connectionString).CreateCloudTableClient()
            };
        }
    }
}
