using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Text;
using TEAMModelOS.SDK.Context.Attributes.Azure;

namespace TEAMModelOS.SDK.Extension.JwtAuth.Models
{
    [TableSpaceAttribute(Name = "Common")]
    public class JwtBlackRecord :TableEntity
    {
        public string Jti { get; set; } 
    }
}
