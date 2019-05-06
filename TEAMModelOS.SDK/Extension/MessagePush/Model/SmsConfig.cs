
using Microsoft.WindowsAzure.Storage.Table;

namespace TEAMModelOS.SDK.Extension.MessagePush.Model
{
    /// <summary>
    /// 短信配置
    /// </summary>
    public class SmsConfig : TableEntity
    {
        /// <summary>
        /// 语言
        /// </summary>
        public string Language { get; set; }
        /// <summary>
        /// 语言名称
        /// </summary>
        public string LanguageName { get; set; }
        /// <summary>
        /// 业务code
        /// </summary>
        public string BizCode { get; set; }
        /// <summary>
        /// 业务名称
        /// </summary>
        public string BizName { get; set; }
        /// <summary>
        /// 模板ID
        /// </summary>
        public string Template { get; set; }
        /// <summary>
        /// 业务流水号
        /// </summary>
        public string BizNum { get; set; }
    }
}
