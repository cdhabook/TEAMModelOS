using TEAMModelOS.SDK.Module.AzureBlob.Configuration;
using TEAMModelOS.SDK.Module.AzureBlob.Container;
using TEAMModelOS.SDK.Module.AzureBlob.Interfaces;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Internal;
using TEAMModelOS.SDK.Helper.Security.AESCrypt;
using TEAMModelOS.SDK.Context.Exception;
using Microsoft.AspNetCore.Http;

namespace TEAMModelOS.SDK.Module.AzureBlob.Implements
{
    public class AzureBlobDBRepository : IAzureBlobDBRepository
    {
        private readonly string china = "417A7572654368696E612020202020202020202020202020202020202020202097EB27FCC1F03349787DCD35F4DE22BBDFEDC90F24738B1D7FB9167A2C191BE671B512E17D48B73A002FC98867345CD59D3250AF59FD5FDFFC67976108F9E3BC9E9F75EDE605B058C1821D16BD9EB753B8E7D39FF48163430C1B5F3B6150195B880C3FCB87D35BF3540432734B768EC28C77B4CF0D556E794DE57979C1E01C429E66F7B2794D9940CF287F2B22A22E5F266B949D5E523E709FF37229E45D1A8FC8C4341E0A8346BB976CCB3D91802FFE5A4A28577898B4E942B5BA3A4A7B796FA673782D405060E7F2CBA4F67DF59F47";
        private readonly string global = "417A757265476C6F62616C2020202020202020202020202020202020202020206956019D195ED330AFA660D369B9464FC5E90AB3A106FDDD7978A2772DB186CDAE21C6CBFDE2B6739F089E853B3171A27841026E61C51666347F63FDF63E4377448D493B05CF6CDB3791946B9145825DD7756392EB8EA36DBF42E5C1C0021CEC2CDB5F4EA57EBCFA98B17D7236FA2CDCA6E7FCBE1DDC45BEAF691A2462A8BC3C429CBC4BCCA3192E554D23758AA8EA5937F988C927534C70A4769ED33878BEC10E2550F121E4AEB5A2DA213F2902D602A758C7D93D5DED368544F8A86D2A0CAA7813D1D950EC81D544EE41A8EDC84173";

        public CloudBlobClient blobClient;
        public CloudBlobContainer blobContainer;
        public AzureBlobOptions _options;
        public AzureBlobDBRepository(AzureBlobOptions options)
        {
            _options = options;
            if (!string.IsNullOrEmpty(options.ConnectionString))
            {
                blobClient = BlobClientSingleton.getInstance(options.ConnectionString).GetBlobClient();
            }
            else if (AzureBlobConfig.AZURE_CHINA.Equals(options.AzureTableDialect))
            {
                AESCrypt crypt = new AESCrypt();
                blobClient = BlobClientSingleton.getInstance(crypt.Decrypt(china, options.AzureTableDialect)).GetBlobClient();
            }
            else if (AzureBlobConfig.AZURE_GLOBAL.Equals(options.AzureTableDialect))
            {
                AESCrypt crypt = new AESCrypt();
                blobClient = BlobClientSingleton.getInstance(crypt.Decrypt(global, options.AzureTableDialect)).GetBlobClient();
            }
            else { throw new BizException("请设置正确的AzureBlob文件存储配置信息！"); }
        }

        public AzureBlobDBRepository()
        {
            // _connectionString = BaseConfigModel.Configuration["AppSettings:Azure:TableStorageConnection"];

        }
        //private async Task InitializeBlob(string container)
        //{
        ////https://teammodelstorage.blob.core.chinacloudapi.cn/wechatfilescontainer
        //    if (blobContainer == null)
        //    {
        //        // Type t = typeof(T);
        //        //若要将权限设置为仅针对 blob 的公共读取访问，请将 PublicAccess 属性设置为 BlobContainerPublicAccessType.Blob。
        //        //要删除匿名用户的所有权限，请将该属性设置为 BlobContainerPublicAccessType.Off。
        //        blobContainer = blobClient.GetContainerReference("wechatfilescontainer");
                
        //       // await blobContainer.CreateIfNotExistsAsync();
        //       // BlobContainerPermissions permissions = await blobContainer.GetPermissionsAsync();
        //       // permissions.PublicAccess = BlobContainerPublicAccessType.Blob;
        //       // await blobContainer.SetPermissionsAsync(permissions);
        //    }
        //    //await UploadFiles(null, new FileContainer() );
        //}

        public async Task<List<AzureBlobModel>> UploadFiles(IFormFile[] file)
        {
            string groupName = DateTime.Now.ToString("yyyyMMdd");
            string newFileName = DateTime.Now.ToString("yyyyMMddHHmmss");
            // await InitializeBlob(DateTime.Now.ToString("yyyyMMdd"));
            blobContainer  = blobClient.GetContainerReference(groupName);
            StorageUri url = blobContainer.StorageUri;
            List<AzureBlobModel> list = new List<AzureBlobModel>();
            foreach (FormFile f in file)
            {
                string[] names = f.FileName.Split(".");
                string name = "";
                for (int i = 0; i < names.Length-1; i++) {
                    name = name + names[i];
                }
                if (names.Length <= 1)
                {
                    name = name + "_" + newFileName;
                }
                else {
                    name = name + "_" + newFileName + "." + names[names.Length - 1];
                }
                var parsedContentDisposition = ContentDispositionHeaderValue.Parse(f.ContentDisposition);
                var filename = Path.Combine(parsedContentDisposition.FileName.Trim('"'));
                var blockBlob = blobContainer.GetBlockBlobReference(name);
                await blockBlob.UploadFromStreamAsync(f.OpenReadStream());
                AzureBlobModel model = new AzureBlobModel(f, _options.Container, groupName, name)
                {
                    BlobUrl = url.PrimaryUri.ToString().Split("?")[0] + "/" + name
                };
                list.Add(model);
            }
            return list;
        }
    }
}
