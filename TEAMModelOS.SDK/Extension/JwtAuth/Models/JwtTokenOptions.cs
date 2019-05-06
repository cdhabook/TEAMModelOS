using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace TEAMModelOS.SDK.Extension.JwtAuth.Models
{
     public  class JwtTokenOptions
    {
        public string Audience { get; set; }
        public RsaSecurityKey Key { get; set; }
        public SigningCredentials Credentials { get; set; }
        public string Issuer { get; set; }
    }
}
