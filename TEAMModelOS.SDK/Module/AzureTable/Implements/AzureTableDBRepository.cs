using TEAMModelOS.SDK.Module.AzureTable.Configuration;
using TEAMModelOS.SDK.Module.AzureTable.Interfaces;
using TEAMModelOS.SDK.Extension.DataResult.PageToken;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TEAMModelOS.SDK.Helper.Security.AESCrypt;
using TEAMModelOS.SDK.Context.Exception;
using System.Reflection;
using TEAMModelOS.SDK.Context.Attributes.Azure;
using TEAMModelOS.SDK.Helper.Common.CollectionHelper;
using TEAMModelOS.SDK.Context.Configuration;
using Microsoft.Extensions.Configuration;

namespace TEAMModelOS.SDK.Module.AzureTable.Implements
{
    public class AzureTableDBRepository : IAzureTableDBRepository
    {
        private readonly string china = "417A7572654368696E612020202020202020202020202020202020202020202097EB27FCC1F03349787DCD35F4DE22BBDFEDC90F24738B1D7FB9167A2C191BE671B512E17D48B73A002FC98867345CD59D3250AF59FD5FDFFC67976108F9E3BC9E9F75EDE605B058C1821D16BD9EB753B8E7D39FF48163430C1B5F3B6150195B880C3FCB87D35BF3540432734B768EC28C77B4CF0D556E794DE57979C1E01C429E66F7B2794D9940CF287F2B22A22E5F266B949D5E523E709FF37229E45D1A8FC8C4341E0A8346BB976CCB3D91802FFE5A4A28577898B4E942B5BA3A4A7B796FA673782D405060E7F2CBA4F67DF59F47";
        private readonly string global = "417A757265476C6F62616C2020202020202020202020202020202020202020206956019D195ED330AFA660D369B9464FC5E90AB3A106FDDD7978A2772DB186CDAE21C6CBFDE2B6739F089E853B3171A27841026E61C51666347F63FDF63E4377448D493B05CF6CDB3791946B9145825DD7756392EB8EA36DBF42E5C1C0021CEC2CDB5F4EA57EBCFA98B17D7236FA2CDCA6E7FCBE1DDC45BEAF691A2462A8BC3C429CBC4BCCA3192E554D23758AA8EA5937F988C927534C70A4769ED33878BEC10E2550F121E4AEB5A2DA213F2902D602A758C7D93D5DED368544F8A86D2A0CAA7813D1D950EC81D544EE41A8EDC84173";

        private readonly CloudTableClient tableClient;
        private CloudTable Table { get; set; }
        //public AzureTableDBRepository(AzureTableOptions options)
        //{
        //    if (!string.IsNullOrEmpty(options.ConnectionString))
        //    {
        //        tableClient = TableClientSingleton.getInstance(options.ConnectionString).GetTableClient();
        //    }
        //    else if (AzureTableConfig.AZURE_CHINA.Equals(options.AzureTableDialect))
        //    {
        //        AESCrypt crypt = new AESCrypt();
        //        tableClient = TableClientSingleton.getInstance(crypt.Decrypt(china, options.AzureTableDialect)).GetTableClient();
        //    }
        //    else if (AzureTableConfig.AZURE_GLOBAL.Equals(options.AzureTableDialect))
        //    {
        //        AESCrypt crypt = new AESCrypt();
        //        tableClient = TableClientSingleton.getInstance(crypt.Decrypt(global, options.AzureTableDialect)).GetTableClient();
        //    }
        //    else { throw new BizException("请设置正确的AzureTable数据库配置信息！"); }

        //}
        public AzureTableDBRepository()
        {
            AzureTableOptions options=  BaseConfigModel.Configuration.GetSection("Azure:Table").Get<AzureTableOptions>();
            if (!string.IsNullOrEmpty(options.ConnectionString))
            {
                tableClient = TableClientSingleton.getInstance(options.ConnectionString).GetTableClient();
            }
            else if (AzureTableConfig.AZURE_CHINA.Equals(options.AzureTableDialect))
            {
                AESCrypt crypt = new AESCrypt();
                tableClient = TableClientSingleton.getInstance(crypt.Decrypt(china, options.AzureTableDialect)).GetTableClient();
            }
            else if (AzureTableConfig.AZURE_GLOBAL.Equals(options.AzureTableDialect))
            {
                AESCrypt crypt = new AESCrypt();
                tableClient = TableClientSingleton.getInstance(crypt.Decrypt(global, options.AzureTableDialect)).GetTableClient();
            }
            else { throw new BizException("请设置正确的AzureTable数据库配置信息！"); }
        }
        private async Task<string> InitializeTable<T>()
        {

            string TableName = GetTableSpace<T>();
            if (Table == null || !Table.Name.Equals(TableName))
            {
                Table = tableClient.GetTableReference(TableName);
                await Table.CreateIfNotExistsAsync();
            }
            return TableName;
        }

        private string GetTableSpace<T>()
        {
            Type type = typeof(T);
            string Name = type.Name;

            object[] attributes = type.GetCustomAttributes(true);
            foreach (object attribute in attributes) //2.通过映射，找到成员属性上关联的特性类实例，
            {
                if (attribute is TableSpaceAttribute tableSpace)
                {
                    Name = tableSpace.Name + Name;
                }
            }
            return Name;
        }

        public async Task<List<T>> FindAll<T>() where T : TableEntity, new()
        {
            string TableName = await InitializeTable<T>();
            var exQuery = new TableQuery<T>();
            return await QueryList<T>(exQuery, TableName);
        }

        private async Task<List<T>> QueryList<T>(TableQuery<T> exQuery, string TableName) where T : TableEntity, new()
        {
            TableContinuationToken continuationToken = null;
            List<T> entitys = new List<T>();
            do
            {
                var result = await tableClient.GetTableReference(TableName).ExecuteQuerySegmentedAsync(exQuery, continuationToken);
                if (result.Results.Count > 0)
                {
                    entitys.AddRange(result.ToList());
                }
                continuationToken = result.ContinuationToken;
            } while (continuationToken != null);
            return entitys;
        }


        private async Task<T> QueryObject<T>(TableQuery<T> exQuery, string TableName) where T : TableEntity, new()
        {
            TableContinuationToken continuationToken = null;
            T entity = new T();
            do
            {
                var result = await tableClient.GetTableReference(TableName).ExecuteQuerySegmentedAsync(exQuery, continuationToken);
                if (result.Results.Count > 0)
                {
                    entity = result.ToList().Single();
                }
                continuationToken = result.ContinuationToken;
            } while (continuationToken != null);
            return entity;
        }

        public async Task<int> Count<T>(TableContinuationToken continuationToken) where T : TableEntity, new()
        {
            string TableName = await InitializeTable<T>();
            var exQuery = new TableQuery<T>();
            List<T> entitys = new List<T>();
            do
            {
                var result = await tableClient.GetTableReference(TableName).ExecuteQuerySegmentedAsync(exQuery, continuationToken);
                if (result.Results.Count > 0)
                {
                    entitys.AddRange(result.ToList());
                }
                continuationToken = result.ContinuationToken;
            } while (continuationToken != null);
            return entitys.Count;
        }

        public async Task<int> Count<T>() where T : TableEntity, new()
        {
            string TableName = await InitializeTable<T>();
            TableContinuationToken continuationToken = null;
            var exQuery = new TableQuery<T>();
            List<T> entitys = new List<T>();
            do
            {
                var result = await tableClient.GetTableReference(TableName).ExecuteQuerySegmentedAsync(exQuery, continuationToken);
                if (result.Results.Count > 0)
                {
                    entitys.AddRange(result.ToList());
                }
                continuationToken = result.ContinuationToken;
            } while (continuationToken != null);
            return entitys.Count;
        }

        public async Task<T> FindByRowKey<T>(string id) where T : TableEntity, new()
        {
            string TableName = await InitializeTable<T>();
            var exQuery = new TableQuery<T>();
            if (!string.IsNullOrEmpty(id))
            {
                string typeStr = SwitchType<T>(id, "RowKey");
                if (string.IsNullOrEmpty(typeStr))
                {
                    return null;
                }
                exQuery.Where(typeStr);
                // exQuery.Where(TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.Equal, id));
                return await QueryObject<T>(exQuery, TableName);
            }
            else
            {
                return null;
            }


        }

        public async Task<List<T>> FindListByDict<T>(Dictionary<string, object> dict) where T : TableEntity, new()
        {
            string TableName = await InitializeTable<T>();
            var exQuery = new TableQuery<T>();
            StringBuilder builder = new StringBuilder();
            if (null != dict && dict.Count > 0)
            {
                var keys = dict.Keys;
                int index = 1;
                foreach (string key in keys)
                {
                    if (dict[key] != null && !string.IsNullOrEmpty(dict[key].ToString()))
                    {
                        string typeStr = SwitchType<T>(dict[key], key);
                        if(string.IsNullOrEmpty(typeStr))
                        {
                            continue;
                        }
                        if (index == 1)
                        {
                            //builder.Append(TableQuery.GenerateFilterCondition(key, QueryComparisons.Equal, dict[key].ToString()));
                            builder.Append(typeStr);

                        }
                        else
                        {
                            //builder.Append("  " + TableOperators.And + "  " + TableQuery.GenerateFilterCondition(key, QueryComparisons.Equal, dict[key].ToString()));
                            builder.Append("  " + TableOperators.And + "  " + typeStr);

                        }
                        index++;
                    }
                    else {
                        throw new Exception("The parameter must have value!");
                    }
                }

                exQuery.Where(builder.ToString());
                return await QueryList<T>(exQuery, TableName);
            }
            else
            {
                return null;
            }
        }

        public async Task<List<T>> FindListByKey<T>(string key, object value) where T : TableEntity, new()
        {
            string TableName = await InitializeTable<T>();

            var exQuery = new TableQuery<T>();
            if (!string.IsNullOrEmpty(key) && value != null && !string.IsNullOrEmpty(value.ToString()))
            {

                string typeStr = SwitchType<T>(value, key);
                if (string.IsNullOrEmpty(typeStr))
                {
                    return null; 
                }
                exQuery.Where(typeStr);
                //exQuery.Where(TableQuery.GenerateFilterCondition(key, QueryComparisons.Equal, value));
                return await QueryList<T>(exQuery, TableName);
            }
            else
            {
                return null;
            }

        }

        public async Task<T> FindOneByDict<T>(IDictionary<string, object> dict) where T : TableEntity, new()
        {


            string TableName = await InitializeTable<T>();
            var exQuery = new TableQuery<T>();
            StringBuilder builder = new StringBuilder();
            if (null != dict && dict.Count > 0)
            {
                var keys = dict.Keys;
                int index = 1;
                foreach (string key in keys)
                {
                    if (dict[key] != null && !string.IsNullOrEmpty(dict[key].ToString()))
                    {


                        string typeStr = SwitchType<T>(dict[key], key);
                        if (string.IsNullOrEmpty(typeStr))
                        {
                            continue;
                        }
                        if (index == 1)
                        {
                            //builder.Append(TableQuery.GenerateFilterCondition(key, QueryComparisons.Equal, dict[key].ToString()));
                            builder.Append(typeStr);
                        }
                        else
                        {
                            // builder.Append("  " + TableOperators.And + "  " + TableQuery.GenerateFilterCondition(key, QueryComparisons.Equal, dict[key].ToString()));
                            builder.Append("  " + TableOperators.And + "  " + typeStr);
                        }
                        index++;
                    }
                    else
                    {
                        throw new Exception("The parameter must have value!");
                    }
                }
                exQuery.Where(builder.ToString());
                return await QueryObject<T>(exQuery, TableName);
            }
            else
            {
                return null;
            }

        }

        private static string SwitchType<T>(object obj, string key)
        {
            Type objType = typeof(T);
            PropertyInfo property = objType.GetProperty(key);

            //Type s = obj.GetType();
            //TypeCode typeCode = Type.GetTypeCode(s);
            if (property == null) {
                //return null;
                throw new Exception(objType.FullName+" PropertyInfo doesn't include this parameter :" +key);
            }
            TypeCode typeCode = Type.GetTypeCode(property.PropertyType);


            switch (typeCode)
            {

                case TypeCode.String: return TableQuery.GenerateFilterCondition(key, QueryComparisons.Equal, obj.ToString());
                case TypeCode.Int32: return TableQuery.GenerateFilterConditionForInt(key, QueryComparisons.Equal, int.Parse( obj.ToString()));
                case TypeCode.Double: return TableQuery.GenerateFilterConditionForDouble(key, QueryComparisons.Equal, (double)obj);
                case TypeCode.Byte: return TableQuery.GenerateFilterConditionForBinary(key, QueryComparisons.Equal, (byte[])obj);
                case TypeCode.Boolean: return TableQuery.GenerateFilterConditionForBool(key, QueryComparisons.Equal, (bool)obj);
                case TypeCode.DateTime: return TableQuery.GenerateFilterConditionForDate(key, QueryComparisons.Equal, (DateTimeOffset)obj);
                case TypeCode.Int64: return TableQuery.GenerateFilterConditionForLong(key, QueryComparisons.Equal, long.Parse(obj.ToString()));

                default: return null;
            }
        }

        public async Task<T> FindOneByKey<T>(string key, object value) where T : TableEntity, new()
        {
            string TableName = await InitializeTable<T>();
            var exQuery = new TableQuery<T>();
            if (!string.IsNullOrEmpty(key) && value != null && !string.IsNullOrEmpty(value.ToString()))
            {

                string typeStr = SwitchType<T>(value, key);
                if (string.IsNullOrEmpty(typeStr))
                {
                   return  null;
                }
                exQuery.Where(typeStr);
                //exQuery.Where(TableQuery.GenerateFilterCondition(key, QueryComparisons.Equal,
                //                                                          value));


                return await QueryObject<T>(exQuery, TableName);
            }
            else
            {
                return null;
            }

        }

        public async Task<List<T>> GetEntities<T>(IDictionary<string, object> dict) where T : TableEntity, new()
        {
            string TableName = await InitializeTable<T>();
            var exQuery = new TableQuery<T>();
            StringBuilder builder = new StringBuilder();
            if (null != dict && dict.Count > 0)
            {
                var keys = dict.Keys;
                int index = 1;
                foreach (string key in keys)
                {
                    if (dict[key] != null && !string.IsNullOrEmpty(dict[key].ToString()))
                    {
                        string typeStr = SwitchType<T>(dict, key);
                        if (string.IsNullOrEmpty(typeStr))
                        {
                            continue;
                        }
                        if (index == 1)
                        {
                            //builder.Append(TableQuery.GenerateFilterCondition(key, QueryComparisons.Equal, dict[key].ToString()));
                            builder.Append(typeStr);

                        }
                        else
                        {
                            // builder.Append("  " + TableOperators.And + "  " + TableQuery.GenerateFilterCondition(key, QueryComparisons.Equal, dict[key].ToString()));
                            builder.Append("  " + TableOperators.And + "  " + typeStr);

                        }
                        index++;
                    }
                    else
                    {
                        throw new Exception("The parameter must have value!");
                    }
                }
                exQuery.Where(builder.ToString());
                return await QueryList<T>(exQuery, TableName);
            }
            else
            {
                return null;
            }
        }



        public async Task<List<T>> SaveAll<T>(List<T> entitys) where T : TableEntity, new()
        {
            if (entitys.IsEmpty())
            {
                return null;
            }
            string TableName = await InitializeTable<T>();
            IList<Dictionary<string, List<T>>> listInfo = new List<Dictionary<string, List<T>>>();
            foreach (IGrouping<string, T> group in entitys.GroupBy(c => c.PartitionKey))
            {
                Dictionary<string, List<T>> dictInfo = new Dictionary<string, List<T>>
                {
                    { group.Key, group.ToList() }
                };
                listInfo.Add(dictInfo);
            }

            foreach (Dictionary<string, List<T>> dict in listInfo)
            {
                IList<TableResult> result = null;
                foreach (string key in dict.Keys)
                {
                    List<T> values = dict[key];
                    //Parallel.ForEach(Partitioner.Create(0, values.Count, 100),
                    //  async range =>
                    //  {
                    //      TableBatchOperation batchOperation = new TableBatchOperation();
                    //      for (Int32 i = range.Item1; i < range.Item2; i++)
                    //          batchOperation.Insert(values[i]);
                    //      result = await tableClient.GetTableReference(TableName).ExecuteBatchAsync(batchOperation);
                    //  });
                    int pageSize = 100;
                    int pages = (int)Math.Ceiling((double)values.Count / pageSize);
                    for (int i = 0; i < pages; i++)
                    {
                        List<T> lists = values.Skip((i) * pageSize).Take(pageSize).ToList();
                        TableBatchOperation batchOperation = new TableBatchOperation();
                        for (int j = 0; j < lists.Count; j++)
                        {
                            batchOperation.Insert(lists[j]);
                        }
                        result = await tableClient.GetTableReference(TableName).ExecuteBatchAsync(batchOperation);
                    }
                }
            }
            return entitys;
        }

        public async Task<List<T>> UpdateAll<T>(List<T> entitys) where T : TableEntity, new()
        {
            if (entitys.IsEmpty())
            {
                return null;
            }
            string TableName = await InitializeTable<T>();
            IList<Dictionary<string, List<T>>> listInfo = new List<Dictionary<string, List<T>>>();
            foreach (IGrouping<string, T> group in entitys.GroupBy(c => c.PartitionKey))
            {
                Dictionary<string, List<T>> dictInfo = new Dictionary<string, List<T>>
                {
                    { group.Key, group.ToList() }
                };
                listInfo.Add(dictInfo);
            }

            foreach (Dictionary<string, List<T>> dict in listInfo)
            {
                IList<TableResult> result = null;
                foreach (string key in dict.Keys)
                {
                    List<T> values = dict[key];
                    //Parallel.ForEach(Partitioner.Create(0, values.Count, 100),
                    //  async range =>
                    //  {
                    //      TableBatchOperation batchOperation = new TableBatchOperation();
                    //      for (Int32 i = range.Item1; i < range.Item2; i++)
                    //          batchOperation.Replace(values[i]);
                    //      result = await tableClient.GetTableReference(TableName).ExecuteBatchAsync(batchOperation);
                    //  });
                    int pageSize = 100;
                    int pages = (int)Math.Ceiling((double)values.Count / pageSize);
                    for (int i = 0; i < pages; i++)
                    {
                        List<T> lists = values.Skip((i) * pageSize).Take(pageSize).ToList();
                        TableBatchOperation batchOperation = new TableBatchOperation();
                        for (int j = 0; j < lists.Count; j++)
                        {
                            batchOperation.Replace(lists[j]);
                        }
                        result = await tableClient.GetTableReference(TableName).ExecuteBatchAsync(batchOperation);
                    }
                }
            }
            return entitys;
        }

        public async Task<List<T>> SaveOrUpdateAll<T>(List<T> entitys) where T : TableEntity, new()
        {
            if (entitys.IsEmpty())
            {
                return null;
            }
            string TableName = await InitializeTable<T>();
            IList<Dictionary<string, List<T>>> listInfo = new List<Dictionary<string, List<T>>>();
            foreach (IGrouping<string, T> group in entitys.GroupBy(c => c.PartitionKey))
            {
                Dictionary<string, List<T>> dictInfo = new Dictionary<string, List<T>>
                {
                    { group.Key, group.ToList() }
                };
                listInfo.Add(dictInfo);
            }

            foreach (Dictionary<string, List<T>> dict in listInfo)
            {
                IList<TableResult> result = null;
                foreach (string key in dict.Keys)
                {
                    List<T> values = dict[key];
                    //Parallel.ForEach(Partitioner.Create(0, values.Count, 50),
                    //  async range =>
                    //  {
                    //      TableBatchOperation batchOperation = new TableBatchOperation();
                    //      for (Int32 i = range.Item1; i < range.Item2; i++)
                    //          batchOperation.InsertOrReplace(values[i]);
                    //      result = await tableClient.GetTableReference(TableName).ExecuteBatchAsync(batchOperation);
                    //  });
                  
                    int pageSize = 100; 
                    int pages = (int)Math.Ceiling((double)values.Count/ pageSize);
                    for (int i= 0; i < pages; i++) {
                        List<T> lists=  values.Skip((i) * pageSize).Take(pageSize).ToList();
                        TableBatchOperation batchOperation = new TableBatchOperation();
                        for (int j = 0; j < lists.Count; j++)
                        {
                            batchOperation.InsertOrReplace(lists[j]);
                        }
                        result = await tableClient.GetTableReference(TableName).ExecuteBatchAsync(batchOperation);
                    }
                }
            }
            return entitys;
        }




        public async Task<List<T>> DeleteAll<T>(List<T> entitys) where T : TableEntity, new()
        {
            if (entitys.IsEmpty())
            {
                return null;
            }
            string TableName = await InitializeTable<T>();
            IList<Dictionary<string, List<T>>> listInfo = new List<Dictionary<string, List<T>>>();
            foreach (IGrouping<string, T> group in entitys.GroupBy(c => c.PartitionKey))
            {
                Dictionary<string, List<T>> dictInfo = new Dictionary<string, List<T>>
                {
                    { group.Key, group.ToList() }
                };
                listInfo.Add(dictInfo);
            }

            foreach (Dictionary<string, List<T>> dict in listInfo)
            {
                IList<TableResult> result = null;
                foreach (string key in dict.Keys)
                {
                    List<T> values = dict[key];
                    //Parallel.ForEach(Partitioner.Create(0, values.Count, 100),
                    //  async range =>
                    //  {
                    //      TableBatchOperation batchOperation = new TableBatchOperation();
                    //      for (Int32 i = range.Item1; i < range.Item2; i++)
                    //          batchOperation.Delete(values[i]);
                    //      result = await tableClient.GetTableReference(TableName).ExecuteBatchAsync(batchOperation);
                    //  });

                    int pageSize = 100;
                    int pages = (int)Math.Ceiling((double)values.Count / pageSize);
                    for (int i = 0; i < pages; i++)
                    {
                        List<T> lists = values.Skip((i) * pageSize).Take(pageSize).ToList();
                        TableBatchOperation batchOperation = new TableBatchOperation();
                        for (int j = 0; j < lists.Count; j++)
                        {
                            batchOperation.Delete(lists[j]);
                        }
                        result = await tableClient.GetTableReference(TableName).ExecuteBatchAsync(batchOperation);
                    }
                }
            }
            return entitys;
        }

        public async Task<T> Save<T>(TableEntity entity) where T : TableEntity, new()
        {
            string TableName = await InitializeTable<T>();
            TableOperation operation = TableOperation.Insert(entity);
            TableResult result = await tableClient.GetTableReference(TableName).ExecuteAsync(operation);
            return (T)result.Result;
        }
        public async Task<T> SaveOrUpdate<T>(TableEntity entity) where T : TableEntity, new()
        {
            string TableName = await InitializeTable<T>();
            TableOperation operation = TableOperation.InsertOrReplace(entity);
            TableResult result = await tableClient.GetTableReference(TableName).ExecuteAsync(operation);
            return (T)result.Result;
        }

        public async Task<T> Update<T>(TableEntity entity) where T : TableEntity, new()
        {
            string TableName = await InitializeTable<T>();
            TableOperation operation = TableOperation.Replace(entity);
            TableResult result = await tableClient.GetTableReference(TableName).ExecuteAsync(operation);
            return (T)result.Result;
        }

        public async Task<T> Delete<T>(TableEntity entity) where T : TableEntity, new()
        {
            string TableName = await InitializeTable<T>();
            TableOperation operation = TableOperation.Delete(entity);
            TableResult result = await tableClient.GetTableReference(TableName).ExecuteAsync(operation);
            return (T)result.Result;
        }


        //public async Task<List<T>> FindListByDictAndLike<T>(Dictionary<string, object> dict, Dictionary<string, object> likeDict) where T : TableEntity, new()
        //{
        //    throw new NotImplementedException();
        //}

        //public async Task<List<T>> FindListByDictAndLikeAndNotEQ<T>(Dictionary<string, object> dict, Dictionary<string, object> likeDict, Dictionary<string, object> notEQDict) where T : TableEntity, new()
        //{
        //    throw new NotImplementedException();
        //}

        //public async Task<List<T>> FindListByDictAndLikeAndStartWith<T>(Dictionary<string, object> dict, Dictionary<string, object> likeDict, Dictionary<string, object> startDict) where T : TableEntity, new()
        //{
        //    throw new NotImplementedException();
        //}

        public async Task<AzurePagination<T>> FindListByDict<T>(Dictionary<string, object> dict, AzureTableToken azureTableToken) where T : TableEntity, new()
        {
            string TableName = await InitializeTable<T>();
            var exQuery = new TableQuery<T>();
            StringBuilder builder = new StringBuilder();
            if (null != dict && dict.Count > 0)
            {
                var keys = dict.Keys;
                int index = 1;
                foreach (string key in keys)
                {
                    if (dict[key] != null && !string.IsNullOrEmpty(dict[key].ToString()))
                    {
                        string typeStr = SwitchType<T>(dict, key);
                        if (string.IsNullOrEmpty(typeStr))
                        {
                            continue;
                        }
                        if (index == 1)
                        {
                            // builder.Append(TableQuery.GenerateFilterCondition(key, QueryComparisons.Equal, dict[key].ToString()));
                            builder.Append(typeStr);

                        }
                        else
                        {
                            builder.Append("  " + TableOperators.And + "  " + typeStr);
                            //builder.Append("  " + TableOperators.And + "  " + TableQuery.GenerateFilterCondition(key, QueryComparisons.Equal, dict[key].ToString()));
                        }
                        index++;
                    }
                    else
                    {
                        throw new Exception("The parameter must have value!");
                    }
                }
                exQuery.Where(builder.ToString());
                return await QueryList<T>(azureTableToken, exQuery, TableName);
            }
            else
            {
                return null;
            }

        }
        private async Task<AzurePagination<T>> QueryList<T>(AzureTableToken azureTableToken, TableQuery<T> exQuery, string TableName) where T : TableEntity, new()
        {
            TableContinuationToken tableToken = new HaBookTableContinuationToken(azureTableToken).GetContinuationToken();
            List<T> entitys = new List<T>();

            var result = await tableClient.GetTableReference(TableName).ExecuteQuerySegmentedAsync(exQuery, tableToken);
            if (result.Results.Count > 0)
            {
                entitys.AddRange(result.ToList());
            }
            tableToken = result.ContinuationToken;
            AzurePagination<T> pagination = new AzurePagination<T>
            {
                token = new HaBookTableContinuationToken(tableToken).GetAzureTableToken(),
                data = entitys
            };
            return pagination;
        }
    }
}
