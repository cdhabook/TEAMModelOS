using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace TEAMModelOS.SDK.Extension.JwtAuth.Models
{
    public class ClaimModel
    {
        public ClaimModel() {
            Claims = new List<Claim>();
            Claim = new Dictionary<string, object>();
            Roles = new List<string>();
        }


        /// <summary>
        /// 用户身份信息
        /// </summary>
        public List<Claim> Claims { get; set; }
        /// <summary>
        /// 用户身份信息
        /// </summary>
        public Dictionary<string ,object> Claim { get; set; }
        /// <summary>
        /// 用户角色信息
        /// </summary>
        public List<string> Roles { get; set; }
        /// <summary>
        /// 令牌类型
        /// </summary>
        public string Scope { get; set; }
    }
}
