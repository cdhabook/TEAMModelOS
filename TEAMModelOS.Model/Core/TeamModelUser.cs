using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace TEAMModelOS.Model.Core
{
    /// <summary>
    /// FreeSQL https://www.cnblogs.com/kellynic/p/10645049.html
    /// </summary>
    [SugarTable("CoreTeamModelUser")]
    public class TeamModelUser
    {
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        /// <summary>
        /// 真实姓名
        /// </summary>
        public string FullName { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public string Sex { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        public string Cellphone { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 醍摩豆ID
        /// </summary>
        public string TeamModelId { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        public string Header { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string CountryId { get; set; }
        /// <summary>
        /// 国家
        /// </summary>
        public string CountryName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ProvinceId { get; set; }
        /// <summary>
        /// 省
        /// </summary>
        public string ProvinceName { get; set; }
        /// <summary>
        /// 市
        /// </summary>
        public string CityId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string CityName { get; set; }
        /// <summary>
        /// 区县
        /// </summary>
        public string DistrictId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string DistrictName { get; set; }
        /// <summary>
        /// 注册时间
        /// </summary>
        public long RegisterTime { get; set; }
        public string CountryCode { get; set; }
    }
}
