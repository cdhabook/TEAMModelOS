using MessagePack;
using Microsoft.AspNetCore.Http;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using TEAMModelOS.SDK.Helper.Common.DateTimeHelper;

namespace TEAMModelOS.SDK.Module.AzureBlob.Container
{
    [MessagePackObject(keyAsPropertyName: true)]
    public class AzureBlobModel : TableEntity
    {


        public AzureBlobModel()
        {
            long time = DateTime.Now.ToUniversalTime().Ticks - 621355968000000000;
            Timestamp = DateTimeHelper.ConvertToDateTime(time);
        }

        public AzureBlobModel(IFormFile f, string Container ,string groupName ,string newName)
        {
            long time= DateTime.Now.ToUniversalTime().Ticks - 621355968000000000;
            ContentType = f.ContentType;
            ContentDisposition = f.ContentDisposition;
            FileName = f.FileName;
            // Headers = f.Headers.Values;
            RealName =  groupName + "/"+ newName;
            Length = f.Length;
            Name = f.Name;
            UploadTime = time;
            PartitionKey = Container;
            RowKey = Guid.NewGuid().ToString();
            Extension = f.FileName.Substring(f.FileName.LastIndexOf(".") + 1, (f.FileName.Length - f.FileName.LastIndexOf(".") - 1)); //扩展名
            Timestamp = DateTimeHelper.ConvertToDateTime(time);
        }

        //
        // 摘要:
        //     Gets the raw Content-Type header of the uploaded file.
        public string BlobUrl { get; set; }

        //
        // 摘要:
        //     Gets the raw Content-Type header of the uploaded file.
        public string ContentType { get; set; }
        //
        // 摘要:
        //     Gets the raw Content-Disposition header of the uploaded file.
        public string ContentDisposition { get; set; }
        //
        // 摘要:
        //     Gets the header dictionary of the uploaded file.
      //  public IHeaderDictionary Headers { get; set; }
        //
        // 摘要:
        //     Gets the file length in bytes.
        public long Length { get; set; }
        //
        // 摘要:
        //     Gets the form field name from the Content-Disposition header.
        public string Name { get; set; }
        //
        // 摘要:
        //     Gets the file name from the Content-Disposition header.
        public string FileName { get; set; }
        public string RealName { get; set; }
        //上传时间戳
        public long UploadTime { get; set; }
        //上传扩展文件
        public string Extension { get; set; }
    }
}
