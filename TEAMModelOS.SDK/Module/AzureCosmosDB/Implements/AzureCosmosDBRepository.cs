using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using TEAMModelOS.SDK.Module.AzureCosmosDB.Configuration;
using TEAMModelOS.SDK.Module.AzureCosmosDB.Interfaces;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents;
using TEAMModelOS.SDK.Helper.Security.AESCrypt;
using TEAMModelOS.SDK.Context.Exception;
using Microsoft.Azure.Documents.Linq;
using TEAMModelOS.SDK.Helper.Query.LinqHelper;
using System.Reflection;
using Microsoft.Azure.CosmosDB.BulkExecutor;
using Microsoft.Azure.CosmosDB.BulkExecutor.BulkImport;
using System.Threading;
using TEAMModelOS.SDK.Helper.Common.JsonHelper;
using Microsoft.Azure.CosmosDB.BulkExecutor.BulkUpdate;
using TEAMModelOS.SDK.Helper.Common.CollectionHelper;
using Microsoft.Azure.CosmosDB.BulkExecutor.BulkDelete;
using TEAMModelOS.SDK.Context.Attributes.Azure;

namespace TEAMModelOS.SDK.Module.AzureCosmosDB.Implements
{ /// <summary>
  /// sdk 文档https://github.com/Azure/azure-cosmos-dotnet-v2/tree/master/samples
  /// https://github.com/Azure/azure-cosmos-dotnet-v2/blob/530c8d9cf7c99df7300246da05206c57ce654233/samples/code-samples/DatabaseManagement/Program.cs#L72-L121
  /// </summary>
    public class AzureCosmosDBRepository : IAzureCosmosDBRepository
    {
        /// <summary>
        /// sdk 文档https://github.com/Azure/azure-cosmos-dotnet-v2/tree/master/samples
        /// https://github.com/Azure/azure-cosmos-dotnet-v2/blob/530c8d9cf7c99df7300246da05206c57ce654233/samples/code-samples/DatabaseManagement/Program.cs#L72-L121
        /// </summary>
       

            private readonly string china_con = "417A7572654368696E6120202020202020202020202020202020202020202020D63873D37F845F9DC7607B4DF4787EE26598454CE32FB5F2EE778A34A5015736196DF7940C67A034CDD4C4B44CD37C20";
            private readonly string china_key = "417A7572654368696E61202020202020202020202020202020202020202020203CAA1DF7E3203F0ABCB2D60C1F3DCB6D90676C4D5467167F6E6A2CB3DDE975EA37B06BBAE6E012936BEDB6D5D60B28B13642F755CB25D1958BE5366EA20FA7C47E04A67B6A96111C61C3270CD0E5539CA45E3A77A6B483F47419BBAEDE75C0F6";
            private readonly string global_con = "417A757265476C6F62616C2020202020202020202020202020202020202020200E357979CB69243DBF4E41BF5526830F89AB746007AC68A3DD3F9CFDA781509F1C48B2359120A5E58B8C7B1EDAA99DEA";
            private readonly string global_key = "417A757265476C6F62616C2020202020202020202020202020202020202020209FF199D61813D1F4857D55CFB0A7D6A797FECF39A7F47553E9C1AF23674CB04BA95748A4A3C07B90F32E5EF26E0982DBF90001E066432075C434351D73FB387D27A50716D90F414F34A4579D846C27804F658705C05A7224EC4D695FD7A5EE23";

            private DocumentClient CosmosClient { get; set; }
            private DocumentCollection CosmosCollection { get; set; }

            private string  Database { get; set; }
            private int CollectionThroughput { get; set; }
            public AzureCosmosDBRepository(AzureCosmosDBOptions options)
            {
                try
                {
                    if (!string.IsNullOrEmpty(options.ConnectionString))
                    {
                        CosmosClient = CosmosDBClientSingleton.getInstance(options.ConnectionString, options.ConnectionKey).GetCosmosDBClient();
                    }
                    else if (AzureCosmosDBConfig.AZURE_CHINA.Equals(options.AzureTableDialect))
                    {
                        AESCrypt crypt = new AESCrypt();
                        CosmosClient = CosmosDBClientSingleton.getInstance(crypt.Decrypt(china_con, options.AzureTableDialect), crypt.Decrypt(china_key, options.AzureTableDialect)).GetCosmosDBClient();
                    }
                    else if (AzureCosmosDBConfig.AZURE_GLOBAL.Equals(options.AzureTableDialect))
                    {
                        AESCrypt crypt = new AESCrypt();
                        CosmosClient = CosmosDBClientSingleton.getInstance(crypt.Decrypt(global_con, options.AzureTableDialect), crypt.Decrypt(global_key, options.AzureTableDialect)).GetCosmosDBClient();
                    }
                    else
                    {
                        throw new BizException("请设置正确的AzureCosmosDB数据库配置信息！");
                    }
                    Database = options.Database;
                CollectionThroughput = options.CollectionThroughput;
                    CosmosClient.CreateDatabaseIfNotExistsAsync(new Database { Id = Database });
                    // _connectionString = options.ConnectionString;
                }
                catch (DocumentClientException de)
                {
                    Exception baseException = de.GetBaseException();
                    Console.WriteLine("{0} error occurred: {1}, Message: {2}", de.StatusCode, de.Message, baseException.Message);
                }
                catch (Exception e)
                {
                    Exception baseException = e.GetBaseException();
                    Console.WriteLine("Error: {0}, Message: {1}", e.Message, baseException.Message);
                }
                finally
                {
                    Console.WriteLine("End of demo, press any key to exit.");
                    //  Console.ReadKey();
                }


            }
            private async Task<DocumentCollection> InitializeCollection<T>()
            {
                Type t = typeof(T);
                if (CosmosCollection == null || !CosmosCollection.Id.Equals(t.Name))
                {
                    DocumentCollection collectionDefinition = new DocumentCollection { Id = t.Name };
                    collectionDefinition.IndexingPolicy = new IndexingPolicy(new RangeIndex(DataType.String) { Precision = -1 });
                    string partitionKey = GetPartitionKey<T>();
                    // collectionDefinition.PartitionKey = new PartitionKeyDefinition {  Paths = new System.Collections.ObjectModel.Collection<string>() };
                    if (!string.IsNullOrEmpty(partitionKey))
                    {
                        collectionDefinition.PartitionKey.Paths.Add("/" + partitionKey);

                    }
                    // CosmosCollection = await this.CosmosClient.CreateDocumentCollectionIfNotExistsAsync(UriFactory.CreateDatabaseUri(Database), collectionDefinition);
                    CosmosCollection = await this.CosmosClient.CreateDocumentCollectionIfNotExistsAsync(
                        UriFactory.CreateDatabaseUri(Database), collectionDefinition, new RequestOptions { OfferThroughput = CollectionThroughput }
                        );
                }

                return CosmosCollection;
            }

            private string GetPartitionKey<T>()
            {
                Type type = typeof(T);
                PropertyInfo[] properties = type.GetProperties();
                List<PropertyInfo> attrProperties = new List<PropertyInfo>();
                foreach (PropertyInfo property in properties)
                {
                    if (property.Name.Equals("PartitionKey"))
                    {
                        attrProperties.Add(property);
                        break;
                    }
                    object[] attributes = property.GetCustomAttributes(true);
                    foreach (object attribute in attributes) //2.通过映射，找到成员属性上关联的特性类实例，
                    {
                        if (attribute is PartitionKeyAttribute)
                        {
                            attrProperties.Add(property);
                        }
                    }
                }

                if (attrProperties.Count <= 0)
                {
                    return null;
                }
                else
                {
                    if (attrProperties.Count == 1)
                    {
                        return attrProperties[0].Name;
                    }
                    else { throw new BizException("PartitionKey can only be single!"); }
                }
            }
            public async Task<T> Save<T>(T entity) //where T : object, new()
            {
                Type t = typeof(T);
                DocumentCollection documentCollection = await InitializeCollection<T>();
                ResourceResponse<Document> doc =
                    await CosmosClient.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(Database, t.Name), entity);
                //Console.WriteLine(doc.ActivityId);


                return entity;
            }

            public async Task<T> Update<T>(T entity)
            {
                Type t = typeof(T);
                await InitializeCollection<T>();
                ResourceResponse<Document> doc =
                    await CosmosClient.UpsertDocumentAsync(UriFactory.CreateDocumentCollectionUri(Database, t.Name), entity);
                return entity;
            }
            public async Task<string> ReplaceObject<T>(T entity, string key)
            {
                Type t = typeof(T);
                await InitializeCollection<T>();
                try
                {
                    ResourceResponse<Document> doc =
                    await CosmosClient.ReplaceDocumentAsync(UriFactory.CreateDocumentUri(Database, t.Name, key), entity);
                    return key;
                }
                catch (Exception e)
                {
                    Console.WriteLine("{0} Exception caught.", e);
                    //return false;
                }
                return null;

            }

            public async Task<string> ReplaceObject<T>(T entity, string key, string partitionKey)
            {
                Type t = typeof(T);
                await InitializeCollection<T>();
                try
                {
                    ResourceResponse<Document> doc =
                    await CosmosClient.ReplaceDocumentAsync(UriFactory.CreateDocumentUri(Database, t.Name, key),
                    entity,
                    new RequestOptions { PartitionKey = new PartitionKey(partitionKey) });
                    return key;
                }
                catch (Exception e)
                {
                    Console.WriteLine("{0} Exception caught.", e);
                    //return false;
                }
                return null;

            }

            public async Task<List<T>> FindAll<T>()
            {
                Type t = typeof(T);
                Boolean open = true;
                List<T> objs = new List<T>();

                //await InitializeCollection<T>();
                //查询条数 -1是全部
                FeedOptions queryOptions = new FeedOptions { MaxItemCount = -1, EnableCrossPartitionQuery = open };
                var query = CosmosClient.CreateDocumentQuery<T>(UriFactory.CreateDocumentCollectionUri(Database, t.Name), queryOptions).AsDocumentQuery();
                while (query.HasMoreResults)
                {
                    foreach (T obj in await query.ExecuteNextAsync())
                    {
                        objs.Add(obj);
                    }
                }
                return objs;
                //return CosmosClient.CreateDocumentQuery<T>(UriFactory.CreateDocumentCollectionUri(Database, t.Name),sql);

            }

            public async Task<List<T>> FindLinq<T>(Func<IQueryable<object>, object> singleOrDefault)
            {
                Type t = typeof(T);

                List<T> objs = new List<T>();
                await InitializeCollection<T>();
                //查询条数 -1是全部
                FeedOptions queryOptions = new FeedOptions { MaxItemCount = -1 };
                var query = CosmosClient.CreateDocumentQuery<T>(UriFactory.CreateDocumentCollectionUri(Database, t.Name), queryOptions);

                //  query.Where();
                return objs;
                //return CosmosClient.CreateDocumentQuery<T>(UriFactory.CreateDocumentCollectionUri(Database, t.Name),sql);

            }

            public async Task<List<T>> FindSQL<T>(string sql)
            {
                Type t = typeof(T);
                List<T> objs = new List<T>();
                await InitializeCollection<T>();
                var query = CosmosClient.CreateDocumentQuery<T>(UriFactory.CreateDocumentCollectionUri(Database, t.Name), sql);
                foreach (var item in query)
                {
                    objs.Add(item);
                }
                return objs;

            }

            public async Task<List<T>> FindSQL<T>(string sql, bool IsPk)
            {
                Type t = typeof(T);
                List<T> objs = new List<T>();
                Boolean open = IsPk;
                await InitializeCollection<T>();
                //查询条数 -1是全部
                FeedOptions queryOptions = new FeedOptions { MaxItemCount = -1, EnableCrossPartitionQuery = open };
                var query = CosmosClient.CreateDocumentQuery<T>(UriFactory.CreateDocumentCollectionUri(Database, t.Name), sql, queryOptions);
                foreach (var item in query)
                {
                    objs.Add(item);
                }
                return objs;

            }



            public async Task<List<T>> FindByparams<T>(Dictionary<string, object> dict)
            {
                //await InitializeCollection<T>();
                Type t = typeof(T);
                Boolean open = true;
                List<Filter> filters = new List<Filter>();

                string PKname = "";
                PropertyInfo[] properties = t.GetProperties();
                foreach (PropertyInfo property in properties)
                {
                    object[] attributes = property.GetCustomAttributes(true);
                    foreach (object attribute in attributes) //2.通过映射，找到成员属性上关联的特性类实例，
                    {
                        if (attribute is PartitionKeyAttribute)
                        {
                            PKname = property.Name;
                            break;
                        }
                    }
                }

                foreach (string key in dict.Keys)
                {
                    //if (t.Name.Equals(key)) {
                    //    open = false;
                    //}

                    if (PKname.Equals(key))
                    {
                        open = false;
                    }
                    filters.Add(new Filter { Key = key, Value = dict[key] != null ? dict[key].ToString() : throw new Exception("参数值不能为null") });
                }


                //List<T> objs = new List<T>();
                await InitializeCollection<T>();
                //查询条数 -1是全部
                FeedOptions queryOptions = new FeedOptions { MaxItemCount = -1, EnableCrossPartitionQuery = open };
                var query = CosmosClient.CreateDocumentQuery<T>(UriFactory.CreateDocumentCollectionUri(Database, t.Name), queryOptions);

                List<T> list = DynamicLinq.GenerateFilter<T>(query, filters).ToList();
                return list;
                //return CosmosClient.CreateDocumentQuery<T>(UriFactory.CreateDocumentCollectionUri(Database, t.Name),sql);

            }
            public async Task<string> DeleteAsync<T>(string id)
            {
                Type t = typeof(T);
                await InitializeCollection<T>();
                ResourceResponse<Document> doc =
                    await CosmosClient.DeleteDocumentAsync(UriFactory.CreateDocumentUri(Database, t.Name, id));
                //Console.WriteLine(doc.ActivityId);
                return id;
            }
            public async Task<T> DeleteAsync<T>(T entity)
            {
                await InitializeCollection<T>();
                Type t = typeof(T);
                string PartitionKey = GetPartitionKey<T>();
                if (!string.IsNullOrEmpty(PartitionKey))
                {
                    string pkValue = entity.GetType().GetProperty(PartitionKey).GetValue(entity).ToString();
                    string idValue = entity.GetType().GetProperty("id").GetValue(entity).ToString();
                    ResourceResponse<Document> doc =
                  await CosmosClient.DeleteDocumentAsync(UriFactory.CreateDocumentUri(Database, t.Name, idValue), new RequestOptions { PartitionKey = new PartitionKey(pkValue) });
                }
                else
                {
                    string idValue = entity.GetType().GetProperty("id").GetValue(entity).ToString();
                    ResourceResponse<Document> doc =
                    await CosmosClient.DeleteDocumentAsync(UriFactory.CreateDocumentUri(Database, t.Name, idValue));
                }


                //Console.WriteLine(doc.ActivityId);
                return entity;

            }
            public async Task<string> DeleteAsync<T>(string id, string partitionKey)
            {
                Type t = typeof(T);

                await InitializeCollection<T>();
                ResourceResponse<Document> doc =
                    await CosmosClient.DeleteDocumentAsync(UriFactory.CreateDocumentUri(Database, t.Name, id), new RequestOptions { PartitionKey = new PartitionKey(partitionKey) });
                //Console.WriteLine(doc.ActivityId);
                return id;

            }

            public async Task<List<T>> SaveAll<T>(List<T> enyites)
            {
                DocumentCollection dataCollection = await InitializeCollection<T>();
                // Set retry options high for initialization (default values).
                CosmosClient.ConnectionPolicy.RetryOptions.MaxRetryWaitTimeInSeconds = 30;
                CosmosClient.ConnectionPolicy.RetryOptions.MaxRetryAttemptsOnThrottledRequests = 9;
                IBulkExecutor bulkExecutor = new BulkExecutor(CosmosClient, dataCollection);
                await bulkExecutor.InitializeAsync();
                // Set retries to 0 to pass control to bulk executor.
                CosmosClient.ConnectionPolicy.RetryOptions.MaxRetryWaitTimeInSeconds = 0;
                CosmosClient.ConnectionPolicy.RetryOptions.MaxRetryAttemptsOnThrottledRequests = 0;
                BulkImportResponse bulkImportResponse = null;
                long totalNumberOfDocumentsInserted = 0;
                double totalRequestUnitsConsumed = 0;
                double totalTimeTakenSec = 0;
                var tokenSource = new CancellationTokenSource();
                var token = tokenSource.Token;
                int pageSize = 100;
                int pages = (int)Math.Ceiling((double)enyites.Count / pageSize);

                for (int i = 0; i < pages; i++)
                {
                    List<string> documentsToImportInBatch = new List<string>();
                    List<T> lists = enyites.Skip((i) * pageSize).Take(pageSize).ToList();
                    for (int j = 0; j < lists.Count; j++)
                    {
                        documentsToImportInBatch.Add(lists[j].ToJson());
                    }
                var tasks = new List<Task>
                { Task.Run(async () =>
                    {
                        do
                        {
                            //try
                            //{
                            bulkImportResponse = await bulkExecutor.BulkImportAsync(
                                documents: documentsToImportInBatch,
                                enableUpsert: true,
                                disableAutomaticIdGeneration: true,
                                maxConcurrencyPerPartitionKeyRange: null,
                                maxInMemorySortingBatchSize: null,
                                cancellationToken: token);
                            //}
                            //catch (DocumentClientException de)
                            //{
                            //    break;
                            //}
                            //catch (Exception e)
                            //{
                            //    break;
                            //}
                        } while (bulkImportResponse.NumberOfDocumentsImported < documentsToImportInBatch.Count);
                        totalNumberOfDocumentsInserted += bulkImportResponse.NumberOfDocumentsImported;
                        totalRequestUnitsConsumed += bulkImportResponse.TotalRequestUnitsConsumed;
                        totalTimeTakenSec += bulkImportResponse.TotalTimeTaken.TotalSeconds;
                    },
                token)
                };
                await Task.WhenAll(tasks);
                }
                return enyites;
            }
            public async Task<List<T>> UpdateAll<T>(Dictionary<string, object> dict, Dictionary<string, object> updateFilters, List<string> deleteKeys = null)
            {

                DocumentCollection dataCollection = await InitializeCollection<T>();
                IBulkExecutor bulkExecutor = new BulkExecutor(CosmosClient, dataCollection);
                await bulkExecutor.InitializeAsync();
                BulkUpdateResponse bulkUpdateResponse = null;
                long totalNumberOfDocumentsUpdated = 0;

                double totalRequestUnitsConsumed = 0;
                double totalTimeTakenSec = 0;

                var tokenSource = new CancellationTokenSource();
                var token = tokenSource.Token;
                // Generate update operations.
                List<UpdateOperation> updateOperations = new List<UpdateOperation>();
                // Unset the description field.
                if (null != updateFilters && updateFilters.Count > 0)
                {
                    var keys = updateFilters.Keys;
                    foreach (string key in keys)
                    {
                        // updateOperations.Add(new SetUpdateOperation<string>())
                        if (updateFilters[key] != null && !string.IsNullOrEmpty(updateFilters[key].ToString()))
                        {
                            updateOperations.Add(SwitchType(key, updateFilters[key]));
                        }
                    }
                }
                if (deleteKeys.IsNotEmpty())
                {
                    foreach (string key in deleteKeys)
                    {
                        updateOperations.Add(new UnsetUpdateOperation(key));
                    }
                }
                List<T> list = await FindByparams<T>(dict);
                int pageSize = 100;
                int pages = (int)Math.Ceiling((double)list.Count / pageSize);
                string partitionKey = "/" + GetPartitionKey<T>();
                Type type = typeof(T);
                for (int i = 0; i < pages; i++)
                {
                    List<UpdateItem> updateItemsInBatch = new List<UpdateItem>();
                    List<T> lists = list.Skip((i) * pageSize).Take(pageSize).ToList();
                    for (int j = 0; j < lists.Count; j++)
                    {
                        string partitionKeyValue = lists[j].GetType().GetProperty(partitionKey).GetValue(lists[j]) + "";
                        string id = lists[j].GetType().GetProperty("id").GetValue(lists[j]) + "";
                        updateItemsInBatch.Add(new UpdateItem(id, partitionKeyValue, updateOperations));
                    }
                var tasks = new List<Task>
                { Task.Run(async () =>
                    {
                        do
                        {
                            //try
                            //{
                            bulkUpdateResponse = await bulkExecutor.BulkUpdateAsync(
                                updateItems: updateItemsInBatch,
                                maxConcurrencyPerPartitionKeyRange: null,
                                cancellationToken: token);
                            //}
                            //catch (DocumentClientException de)
                            //{
                            //    break;
                            //}
                            //catch (Exception e)
                            //{
                            //    break;
                            //}
                        } while (bulkUpdateResponse.NumberOfDocumentsUpdated < updateItemsInBatch.Count);
                        totalNumberOfDocumentsUpdated += bulkUpdateResponse.NumberOfDocumentsUpdated;
                        totalRequestUnitsConsumed += bulkUpdateResponse.TotalRequestUnitsConsumed;
                        totalTimeTakenSec += bulkUpdateResponse.TotalTimeTaken.TotalSeconds;
                    },
                token)
                };
                await Task.WhenAll(tasks);
                }
                return list;
            }
            public async Task<List<T>> DeleteALl<T>(Dictionary<string, object> dict)
            {
                DocumentCollection dataCollection = await InitializeCollection<T>();
                List<T> list = await FindByparams<T>(dict);
                List<Tuple<string, string>> pkIdTuplesToDelete = new List<Tuple<string, string>>();
                if (list.IsNotEmpty())
                {
                    foreach (T t in list)
                    {
                        string id = t.GetType().GetProperty("id").GetValue(t) + "";
                        pkIdTuplesToDelete.Add(new Tuple<string, string>(id, id));
                    }
                }
                else
                {
                    return null;
                }
                long totalNumberOfDocumentsDeleted = 0;
                double totalRequestUnitsConsumed = 0;
                double totalTimeTakenSec = 0;
                BulkDeleteResponse bulkDeleteResponse = null;

                BulkExecutor bulkExecutor = new BulkExecutor(CosmosClient, dataCollection);
                await bulkExecutor.InitializeAsync();
                bulkDeleteResponse = await bulkExecutor.BulkDeleteAsync(pkIdTuplesToDelete);
                totalNumberOfDocumentsDeleted = bulkDeleteResponse.NumberOfDocumentsDeleted;
                totalRequestUnitsConsumed = bulkDeleteResponse.TotalRequestUnitsConsumed;
                totalTimeTakenSec = bulkDeleteResponse.TotalTimeTaken.TotalSeconds;
                return list;
            }
            private static UpdateOperation SwitchType(string key, object obj)
            {
                Type s = obj.GetType();
                TypeCode typeCode = Type.GetTypeCode(s);
                switch (typeCode)
                {
                    case TypeCode.String: return new SetUpdateOperation<string>(key, obj.ToString());
                    case TypeCode.Int32: return new SetUpdateOperation<Int32>(key, (Int32)obj);
                    case TypeCode.Double: return new SetUpdateOperation<Double>(key, (Double)obj);
                    case TypeCode.Byte: return new SetUpdateOperation<Byte>(key, (Byte)obj);
                    case TypeCode.Boolean: return new SetUpdateOperation<Boolean>(key, (Boolean)obj);
                    case TypeCode.DateTime: return new SetUpdateOperation<DateTime>(key, (DateTime)obj);
                    case TypeCode.Int64: return new SetUpdateOperation<Int64>(key, (Int64)obj);
                    default: return null;
                }
            }
        }
    }
