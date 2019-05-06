using System.Collections.Generic;

namespace TEAMModelOS.SDK.Module.GrpcServer.Extensions
{
    public class GrpcIPConfig
    {
        /// <summary>
        /// 黑名单
        /// </summary>
        public List<string> BlackList { get; set; }
        /// <summary>
        /// 白名单
        /// </summary>
        //public List<string> WhiteList { get; set; }
    }
}
