using System;
using System.Collections.Generic;

namespace TEAMModelOS.SDK.Module.LiteDB.Interfaces
{
    public interface ILiteDBOperator
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        string Save<T>(T obj) where T : new();
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IList<T> FindAll<T>() where T : new();
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        int SaveAll<T>(List<T> obj) where T : new();
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        bool Update<T>(T obj) where T : new();
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        int UpdateAll<T>(List<T> obj) where T : new();
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="docId"></param>
        /// <returns></returns>
        bool Delete<T>(Object docId);
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="docId"></param>
        /// <returns></returns>
        T FindById<T>(dynamic docId) where T : new();
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dict"></param>
        /// <returns></returns>
        T FindOneByDict<T>(Dictionary<String, Object> dict) where T : new();
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        T FindOneByKey<T>(String key, String value) where T : new();
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dict"></param>
        /// <returns></returns>
        IList<T> FindListByDict<T>(Dictionary<String, Object> dict) where T : new();
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        IList<T> FindListByKey<T>(String key, String value) where T : new();
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dict"></param>
        /// <param name="likeDict"></param>
        /// <returns></returns>
        IList<T> FindListByDictAndLike<T>(Dictionary<String, Object> dict, Dictionary<String, Object> likeDict) where T : new();
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dict"></param>
        /// <param name="likeDict"></param>
        /// <param name="startDict"></param>
        /// <returns></returns>
        IList<T> FindListByDictAndLikeAndStartWith<T>( Dictionary<String, Object> dict, Dictionary<String, Object> likeDict, Dictionary<String, Object> startDict) where T : new();
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dict"></param>
        /// <param name="likeDict"></param>
        /// <param name="notEQDict"></param>
        /// <returns></returns>
        IList<T> FindListByDictAndLikeAndNotEQ<T>(Dictionary<String, Object> dict,Dictionary<String, Object> likeDict,Dictionary<String, Object> notEQDict)where T : new();
    }
}
