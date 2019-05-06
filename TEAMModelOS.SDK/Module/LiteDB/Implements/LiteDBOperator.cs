using TEAMModelOS.SDK.Module.LiteDB.Configuration;
using TEAMModelOS.SDK.Module.LiteDB.Interfaces;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using TEAMModelOS.SDK.Helper.Common.JsonHelper;
using TEAMModelOS.SDK.Context.Exception;

namespace TEAMModelOS.SDK.Module.LiteDB.Implements
{
    public class LiteDBOperator : ILiteDBOperator
    {
        private readonly LiteDatabase database;
        public LiteDBOperator(LiteDBOptions options)
        {
            database = LiteDatabaseSingleton.GetInstance(options.ConnectionString).GetDatabase();
        }

        /// <summary>
        /// 保存单个对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public  string Save<T>(T obj)
           where T : new()
        {
            //var db = LiteDBSingleton.getInstance().GetLiteDatabase();
            // Open data file (or create if not exits)
            Type t = typeof(T);
            // Get a collection (or create, if not exits)
            var col = database.GetCollection<T>(t.Name.ToString());
            // Insert new customer document
            var value = col.Insert(obj);
            return value.ToString();
        }
        /// <summary>
        /// 获取全部数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public  IList<T> FindAll<T>()
            where T : new()
        {
            // Open data file (or create if not exits)
          //  var db = LiteDBSingleton.getInstance().GetLiteDatabase();
            Type t = typeof(T);
            // Get a collection (or create, if not exits)
            var col = database.GetCollection<T>(t.Name.ToString());
            var docs = col.FindAll();
            return docs.ToList();
        }



        /// <summary>
        /// 保存单个对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public   int SaveAll<T>(List<T> obj)
           where T : new()
        {
            // Open data file (or create if not exits)

          //  var db = LiteDBSingleton.getInstance().GetLiteDatabase();
            Type t = typeof(T);
            // Get a collection (or create, if not exits)
            var col = database.GetCollection<T>(t.Name.ToString());
            // Insert new customer document

            var value = col.Insert(obj);
            return value;




        }

        /// <summary>
        /// 更新单个对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public   bool Update<T>(T obj)
            where T : new()
        {
            // Open data file (or create if not exits)
           // var db = LiteDBSingleton.getInstance().GetLiteDatabase();
            Type t = typeof(T);
            // Get a collection (or create, if not exits)
            var col = database.GetCollection<T>(t.Name.ToString());
            // Update a document inside a collection
            try
            {
                var success = col.Update(obj);
                return success;
            }
            catch (Exception e) {
                var s = e.Message;
            }
            return false;

        }

        /// <summary>
        /// 更新多个对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public   int UpdateAll<T>(List<T> obj)
            where T : new()
        {
            // Open data file (or create if not exits)
          //  var db = LiteDBSingleton.getInstance().GetLiteDatabase();
            Type t = typeof(T);
            // Get a collection (or create, if not exits)
            var col = database.GetCollection<T>(t.Name.ToString());
            // Update a document inside a collection
            try
            {
                var success = col.Update(obj);
                return success;
            }
            catch (Exception e) {
                var s = e.Message;
            }
            return 0;

        }

        /// <summary>
        /// 删除对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="docId"></param>
        /// <returns></returns>
        public   bool Delete<T>(dynamic docId)
        {
            // Open data file (or create if not exits)
           // var db = LiteDBSingleton.getInstance().GetLiteDatabase();
            Type t = typeof(T);
            // Get a collection (or create, if not exits)
            var col = database.GetCollection(t.Name.ToString());
            var success = col.Delete(docId);
            return success;

        }

        /// <summary>
        /// 根据ID获取
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="docId"></param>
        /// <returns></returns>
        public   T FindById<T>(dynamic docId)
            where T : new()
        {
            // Open data file (or create if not exits)
         //   var db = LiteDBSingleton.getInstance().GetLiteDatabase();
            Type t = typeof(T);
            // Get a collection (or create, if not exits)
            var col = database.GetCollection(t.Name.ToString());
            var doc = col.FindById(docId);
            //  String inf =  doc.AsDocument.ToString();
            return MessagePackHelper.JsonToObject<T>(doc.ToString());

        }

        /// <summary>
        /// 根据key-value方式获取 匹配查询 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dict"></param>
        /// <returns></returns>
        public   T FindOneByDict<T>(Dictionary<String, Object> dict)
           where T : new()
        {
            // Open data file (or create if not exits)
           // var db = LiteDBSingleton.getInstance().GetLiteDatabase();
            Type t = typeof(T);
            // Get a collection (or create, if not exits)
            var col = database.GetCollection(t.Name.ToString());

            //  var doc = col.FindById(docId);

            //  String inf =  doc.AsDocument.ToString();
            IList<Query> queries = new List<Query>();

            if (dict != null && dict.Count > 0)
            {
                var keys = dict.Keys;
                foreach (String key in dict.Keys)
                {
                    if (dict[key] != null && !string.IsNullOrEmpty(dict[key].ToString()))
                    {
                        queries.Add(Query.EQ(key, dict[key].ToString()));
                    }
                }
            }
            var doc = new List<BsonDocument>();
            if (queries != null && queries.Count > 1)
            {
                doc = col.Find(Query.And(queries.ToArray())).ToList();
            }
            else if (queries != null && queries.Count() == 1)
            {
                doc = col.Find(queries.ToArray()[0]).ToList();
            }
            else
            {
                throw new BizException("请输入查询条件！");
            }
            if (doc != null && doc.Count > 0)
            {
                return MessagePackHelper.JsonToObject<T>(doc[0].ToString());
            }
            else
            {
                return new T();
            }

        }
        /// <summary>
        /// 根据key-value方式获取 匹配查询 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public   T FindOneByKey<T>(String key, String value)
           where T : new()
        {
            // Open data file (or create if not exits)
           // var db = LiteDBSingleton.getInstance().GetLiteDatabase();
            Type t = typeof(T);
            // Get a collection (or create, if not exits)
            var col = database.GetCollection(t.Name.ToString());

            //  var doc = col.FindById(docId);

            //  String inf =  doc.AsDocument.ToString();
            var doc = col.Find(Query.EQ(key, value)).ToList();

            if (doc != null && doc.Count > 0)
            {
                return MessagePackHelper.JsonToObject<T>(doc[0].ToString());
            }
            else
            {
                return new T();
            }

        }


        /// <summary>
        /// 根据key-value方式获取 匹配查询 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dict"></param>
        /// <returns></returns>
        public   IList<T> FindListByDict<T>(Dictionary<String, Object> dict)
          where T : new()
        {
            // Open data file (or create if not exits)
          //  var db = LiteDBSingleton.getInstance().GetLiteDatabase();
            Type t = typeof(T);
            // Get a collection (or create, if not exits)
            var col = database.GetCollection(t.Name.ToString());

            //  var doc = col.FindById(docId);

            //  String inf =  doc.AsDocument.ToString();
            IList<Query> queries = new List<Query>();

            if (dict != null && dict.Count > 0)
            {
                var keys = dict.Keys;
                foreach (String key in dict.Keys)
                {
                    if (dict[key] != null && !string.IsNullOrEmpty(dict[key].ToString()))
                    {
                        queries.Add(Query.EQ(key, dict[key].ToString()));
                    }
                }
            }

            var doc = new List<BsonDocument>();
            if (queries != null && queries.Count > 1)
            {
                doc = col.Find(Query.And(queries.ToArray())).ToList();
            }
            else if (queries != null && queries.Count() == 1)
            {
                doc = col.Find(queries.ToArray()[0]).ToList();
            }
            else
            {
                // throw new BizException("请输入查询条件！");
                doc = col.FindAll().ToList();
            }
            return MessagePackHelper.JsonToObject<List<T>>(doc.ToString());

        }
        /// <summary>
        /// 根据key-value方式获取列表 匹配查询 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public   IList<T> FindListByKey<T>(String key, String value)
           where T : new()
        {
            // Open data file (or create if not exits)
           // var db = LiteDBSingleton.getInstance().GetLiteDatabase();
            Type t = typeof(T);
            // Get a collection (or create, if not exits)
            var col = database.GetCollection(t.Name.ToString());
            var doc = col.Find(Query.EQ(key, value)).ToList();
            return MessagePackHelper.JsonToObject<List<T>>(doc.ToString());

        }

        /// <summary>
        /// 根据key-value方式获取 匹配查询 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dict"></param>
        /// <returns></returns>
        public   IList<T> FindListByDictAndLike<T>(Dictionary<String, Object> dict, Dictionary<String, Object> likeDict)
            where T : new()
        {
            // Open data file (or create if not exits)
          //  var db = LiteDBSingleton.getInstance().GetLiteDatabase();
            Type t = typeof(T);
            // Get a collection (or create, if not exits)
            var col = database.GetCollection(t.Name.ToString());

            //  var doc = col.FindById(docId);

            //  String inf =  doc.AsDocument.ToString();
            IList<Query> queries = new List<Query>();

            if (null != dict && dict.Count > 0)
            {
                var keys = dict.Keys;
                foreach (String key in keys)
                {
                    if (dict[key] != null && !string.IsNullOrEmpty(dict[key].ToString()))
                    {
                        queries.Add(Query.EQ(key, dict[key].ToString()));
                    }
                }
            }
            if (null != likeDict && likeDict.Count > 0)
            {
                var keys = likeDict.Keys;
                foreach (String key in keys)
                {
                    if (likeDict[key] != null && !string.IsNullOrEmpty(likeDict[key].ToString()))
                    {
                        queries.Add(Query.Contains(key, likeDict[key].ToString()));
                    }
                }
            }
            var doc = new List<BsonDocument>();
            if (queries != null && queries.Count() > 1)
            {
                doc = col.Find(Query.And(queries.ToArray())).ToList();
            }
            else if (queries != null && queries.Count() == 1)
            {
                doc = col.Find(queries.ToArray()[0]).ToList();
            }
            else
            {
                //throw new BizException("请输入查询条件！");
                doc = col.FindAll().ToList();
            }
            return MessagePackHelper.JsonToObject<List<T>>(doc.ToString());

        }


        /// <summary>
        /// 根据key-value方式获取 匹配查询 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dict"></param>
        /// <returns></returns>
        public   IList<T> FindListByDictAndLikeAndStartWith<T>(
            Dictionary<String, Object> dict,
            Dictionary<String, Object> likeDict,
            Dictionary<String, Object> startDict)
            where T : new()
        {
            // Open data file (or create if not exits)
         //   var db = LiteDBSingleton.getInstance().GetLiteDatabase();
            Type t = typeof(T);
            // Get a collection (or create, if not exits)
            var col = database.GetCollection(t.Name.ToString());

            //  var doc = col.FindById(docId);

            //  String inf =  doc.AsDocument.ToString();
            IList<Query> queries = new List<Query>();

            if (null != dict && dict.Count > 0)
            {
                var keys = dict.Keys;
                foreach (String key in keys)
                {
                    if (dict[key] != null && !string.IsNullOrEmpty(dict[key].ToString()))
                    {
                        queries.Add(Query.EQ(key, dict[key].ToString()));
                    }
                }
            }
            if (null != likeDict && likeDict.Count > 0)
            {
                var keys = likeDict.Keys;
                foreach (String key in keys)
                {
                    if (likeDict[key] != null && !string.IsNullOrEmpty(likeDict[key].ToString()))
                    {
                        queries.Add(Query.Contains(key, likeDict[key].ToString()));
                    }
                }
            }
            if (null != startDict && startDict.Count > 0)
            {
                var keys = startDict.Keys;
                foreach (String key in keys)
                {
                    if (startDict[key] != null && !string.IsNullOrEmpty(startDict[key].ToString()))
                    {
                        queries.Add(Query.StartsWith(key, startDict[key].ToString()));
                    }
                }
            }

            var doc = new List<BsonDocument>();
            if (queries != null && queries.Count() > 1)
            {
                doc = col.Find(Query.And(queries.ToArray())).ToList();
            }
            else if (queries != null && queries.Count() == 1)
            {
                doc = col.Find(queries.ToArray()[0]).ToList();
            }
            else
            {
                //throw new BizException("请输入查询条件！");
                doc = col.FindAll().ToList();

            }
            return MessagePackHelper.JsonToObject<List<T>>(doc.ToString());

        }


        /// <summary>
        /// 根据key-value方式获取 匹配查询 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dict"></param>
        /// <returns></returns>
        public   IList<T> FindListByDictAndLikeAndNotEQ<T>(Dictionary<String, Object> dict,
                                                                Dictionary<String, Object> likeDict,
                                                                Dictionary<String, Object> notEQDict)
            where T : new()
        {
            // Open data file (or create if not exits)
           // var db = LiteDatabaseSingleton.getInstance().GetLiteDatabase();
            Type t = typeof(T);
            // Get a collection (or create, if not exits)
            var col = database.GetCollection(t.Name.ToString());

            //  var doc = col.FindById(docId);

            //  String inf =  doc.AsDocument.ToString();
            IList<Query> queries = new List<Query>();

            if (null != dict && dict.Count > 0)
            {
                var keys = dict.Keys;
                foreach (String key in keys)
                {
                    if (dict[key] != null && !string.IsNullOrEmpty(dict[key].ToString()))
                    {
                        queries.Add(Query.EQ(key, dict[key].ToString()));
                    }
                }
            }
            if (null != likeDict && likeDict.Count > 0)
            {
                var keys = likeDict.Keys;
                foreach (String key in keys)
                {
                    if (likeDict[key] != null && !string.IsNullOrEmpty(likeDict[key].ToString()))
                    {
                        queries.Add(Query.Contains(key, likeDict[key].ToString()));
                    }
                }
            }
            if (null != notEQDict && notEQDict.Count > 0)
            {
                var keys = notEQDict.Keys;
                foreach (String key in keys)
                {
                    if (notEQDict[key] != null && !string.IsNullOrEmpty(notEQDict[key].ToString()))
                    {
                        queries.Add(Query.Not(key, notEQDict[key].ToString()));
                    }
                }
            }
            var doc = new List<BsonDocument>();
            if (queries != null && queries.Count() > 1)
            {
                doc = col.Find(Query.And(queries.ToArray())).ToList();
            }
            else if (queries != null && queries.Count() == 1)
            {
                doc = col.Find(queries.ToArray()[0]).ToList();
            }
            else
            {
                // throw new BizException("请输入查询条件！");
                doc = col.FindAll().ToList();
            }
            return MessagePackHelper.JsonToObject<List<T>>(doc.ToString());

        }
    }
}
