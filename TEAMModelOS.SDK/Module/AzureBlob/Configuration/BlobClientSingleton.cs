using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;

namespace TEAMModelOS.SDK.Module.AzureBlob.Configuration
{
    public sealed class BlobClientSingleton
    {
        private static string _connectionString;
        private CloudBlobClient BlobClient;

        private BlobClientSingleton() { }

        public CloudBlobClient GetBlobClient()
        {
            if (BlobClient != null)
            {
                return BlobClient;
            }
            else
            {
                getInstance(_connectionString);
                return BlobClient;
            }
        }

        public static BlobClientSingleton getInstance(string connectionString)
        {
            _connectionString = connectionString;
            return SingletonInstance.instance;
        }

        private static class SingletonInstance
        {
            public static BlobClientSingleton instance = new BlobClientSingleton()
            {
               // BlobClient = CloudStorageAccount.Parse(_connectionString).CreateCloudBlobClient()
               BlobClient = new  CloudBlobClient(new Uri(_connectionString))
            };
        }
    }
}
