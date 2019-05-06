using System;
using System.Collections.Generic;
using System.Text;
using TEAMModelOS.SDK.Context.Attributes.MySQL;

namespace TEAMModelOS.SDK.Module.SqlSugar.Configuration.Data
{
    /// <summary>
    /// 排序方式
    /// </summary>
    public enum DbOrderEnum
    {
        /// <summary>
        /// ASC排序
        /// </summary>
        [Text("排序Asc")]
        Asc = 1,

        /// <summary>
        /// DESC 排序
        /// </summary>
        [Text("排序Desc")]
        Desc = 2
    }
}
