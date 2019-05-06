using TEAMModelOS.SDK.Module.AzureBlob.Container;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TEAMModelOS.SDK.Module.AzureBlob.Interfaces
{
    public interface IAzureBlobDBRepository
    {
        Task<List<AzureBlobModel>> UploadFiles(IFormFile[] file);
    }
}
