using System;
using System.Collections.Generic;
using System.Text;

namespace TEAMModelOS.SDK.Extension.JwtAuth.Models
{
    public class JwtSetting
    {
        /// <summary>
        /// 项目名称
        /// </summary>
       // public string Project { get; set; }
        /// <summary>
        /// JwtClient
        /// </summary>
        public List<JwtClient> JwtClient { get; set; }
        /// <summary>
        /// WT的接收对象
        /// </summary>
        public string Audience { get; set; }
        /// <summary>
        /// JWT的签发主体
        /// </summary>
        public string Issuer { get; set; }
        /// <summary>
        /// JWT Secret Key
        /// </summary>
        //public string SecurityKey { get; set; }
    }
}
