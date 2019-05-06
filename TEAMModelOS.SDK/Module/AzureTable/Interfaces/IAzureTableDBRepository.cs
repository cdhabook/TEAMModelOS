using TEAMModelOS.SDK.Module.AzureTable.Configuration;
using TEAMModelOS.SDK.Extension.DataResult.PageToken;
using Microsoft.WindowsAzure.Storage.Table;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TEAMModelOS.SDK.Module.AzureTable.Interfaces
{
    public interface IAzureTableDBRepository
    {
        Task<List<T>> GetEntities<T>(IDictionary<string, object> dict) where T : TableEntity, new();
        Task<T> FindOneByDict<T>(IDictionary<string, object> dict) where T : TableEntity, new();
        Task<T> FindByRowKey<T>(string RowKey) where T : TableEntity, new();
        Task<T> Save<T>(TableEntity entity) where T : TableEntity, new();
        Task<T> Update<T>(TableEntity entity) where T : TableEntity, new();
        Task<T> Delete<T>(TableEntity entity) where T : TableEntity, new();
        Task<T> FindOneByKey<T>(string key, object value) where T : TableEntity, new();
        Task<List<T>> FindListByDict<T>(Dictionary<string, object> dict) where T : TableEntity, new();
        Task<List<T>> FindListByKey<T>(string key, object value) where T : TableEntity, new();
        Task<List<T>> FindAll<T>() where T : TableEntity, new();
        Task<List<T>> DeleteAll<T>(List<T> entitys) where T : TableEntity, new();
        Task<List<T>> UpdateAll<T>(List<T> entitys) where T : TableEntity, new();
        Task<List<T>> SaveAll<T>(List<T> entitys) where T : TableEntity, new();
        //Task<List<T>> FindListByDictAndLike<T>(
        //    Dictionary<string, object> dict,
        //    Dictionary<string, object> likeDict) where T : TableEntity, new();
        //Task<List<T>> FindListByDictAndLikeAndStartWith<T>(
        //    Dictionary<string, object> dict,
        //    Dictionary<string, object> likeDict,
        //    Dictionary<string, object> startDict) where T : TableEntity, new();
        //Task<List<T>> FindListByDictAndLikeAndNotEQ<T>(
        //    Dictionary<string, object> dict,
        //    Dictionary<string, object> likeDict,
        //    Dictionary<string, object> notEQDict) where T : TableEntity, new();
        //Task<int> Count<T>() where T : TableEntity, new();
        //Task<AzurePagination<T>> FindListByDict<T>(Dictionary<string, object> dict, AzurePagination<T> pagination) where T : TableEntity, new();
        Task<AzurePagination<T>> FindListByDict<T>(Dictionary<string, object> dict, AzureTableToken azureTableToken) where T : TableEntity, new();
        Task<T> SaveOrUpdate<T>(TableEntity entity) where T : TableEntity, new();
        Task<List<T>> SaveOrUpdateAll<T>(List<T> entitys) where T : TableEntity, new();
    }
}
