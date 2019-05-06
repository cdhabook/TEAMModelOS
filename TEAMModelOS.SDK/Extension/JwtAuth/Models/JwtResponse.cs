using System;
using System.Collections.Generic;
using System.Text;
using TEAMModelOS.SDK.Context.Constant.Common;

namespace TEAMModelOS.SDK.Extension.JwtAuth.Models
{
    public class JwtResponse
    {
        public string Access_token { get; set; }
        public string Token_type { get; set; } = "Bearer";
        public string Token_key { get; set; } =Constants.AUTHORIZATION;
        public string Scope { get; set; }
    }
}
