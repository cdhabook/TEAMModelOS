using Microsoft.Azure.Documents.Client;
using System;

namespace TEAMModelOS.SDK.Module.AzureCosmosDB.Configuration
{
    public sealed class CosmosDBClientSingleton
    {
        private static string _connectionUrl;
        private static string _connectionKey;
        private DocumentClient CosmosClient;

        private CosmosDBClientSingleton() { }



        public DocumentClient GetCosmosDBClient()
        {
            if (CosmosClient != null)
            {
                return CosmosClient;
            }
            else
            {
                getInstance(_connectionUrl, _connectionKey);
                return CosmosClient;
            }
        }

        public static CosmosDBClientSingleton getInstance(string connectionUrl, string connectionKey)
        {
            _connectionUrl = connectionUrl;
            _connectionKey = connectionKey;
            return SingletonInstance.instance;
        }

        private static class SingletonInstance
        {
            public static CosmosDBClientSingleton instance = new CosmosDBClientSingleton()
            {
                CosmosClient = new DocumentClient(new Uri(_connectionUrl), _connectionKey, ConnectionPolicy)
            };
            private static readonly ConnectionPolicy ConnectionPolicy = new ConnectionPolicy
            {
                ConnectionMode = ConnectionMode.Direct,
                ConnectionProtocol = Protocol.Tcp
            };
        }
    }
}
