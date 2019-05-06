using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace TEAMModelOS.SDK.Module.AzureCosmosDB.Interfaces
{
    public interface IAzureCosmosDBRepository
    {
        Task<T> Save<T>(T entity);
        Task<T> Update<T>(T entity);
        Task<string> ReplaceObject<T>(T entity, string key);
        Task<string> ReplaceObject<T>(T entity, string key, string partitionKey);
        Task<List<T>> FindAll<T>();
        Task<string> DeleteAsync<T>(string id);
        Task<string> DeleteAsync<T>(string id, string partitionKey);
        Task<T> DeleteAsync<T>(T entity);
        Task<List<T>> FindSQL<T>(string sql);
        Task<List<T>> FindSQL<T>(string sql, bool isPK);
        Task<List<T>> FindLinq<T>(Func<IQueryable<object>, object> singleOrDefault);
        Task<List<T>> FindByparams<T>(Dictionary<string, object> dict);
        Task<List<T>> SaveAll<T>(List<T> enyites);
        Task<List<T>> UpdateAll<T>(Dictionary<string, object> dict, Dictionary<string, object> updateFilters, List<string> deleteKeys = null);
        Task<List<T>> DeleteALl<T>(Dictionary<string, object> dict);
    }
}
