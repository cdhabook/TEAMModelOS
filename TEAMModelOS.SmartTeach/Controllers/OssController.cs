using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace TEAMModelOS.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class OssController : Controller
    {
        [HttpPost("Upload")]
        //[RequestSizeLimit(102_400_000_00)] //最大10000m左右
        public string[] BlobSaveFile([FromForm] IFormFile[] files)
        {
            //throw new BizException("1");
            return files.Select(f => f.FileName).ToArray();
        }
    }
}
