using TEAMModelOS.SDK.Extension.MessagePush.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TEAMModelOS.SDK.Extension.MessagePush.Interfaces
{
    public interface ISendCloudService
    {
        /// <summary>
        /// 发送普通文字短信
        /// </summary>
        /// <param name="templateId">模板ID</param>
        /// <param name="msgType">消息类型0表示短信, 1表示彩信,2表示国际短信， 默认值为0 </param>
        /// <param name="phone">信人手机号,多个手机号用逗号,分隔，每次调用最大支持2000，更多地址建议使用联系人列表功能</param>
        /// <param name="vars">替换变量的json串 ,含有特殊字符 请 urlencode ,{"name": "lucy"} or {"%money%": "100"}</param>
        /// <returns></returns>
         Task<SendCloudResponse> SendSms(int templateId, string phone, Dictionary<string, string> vars =null , int msgType = 0);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="BizCode">业务Code</param>
        /// <param name="CountryCode">国家或地区编码</param>
        /// <param name="phone">手机号</param>
        /// <param name="vars">替换变量的json串 ,含有特殊字符 请 urlencode ,{"name": "lucy"} or {"%money%": "100"}</param>
        /// <returns></returns>
        Task<SendCloudResponse> SendSmsByBizCode(string BizNum  , string BizCode, int CountryCode, string phone, Dictionary<string, string> vars = null);
        /// <summary>
        /// 根据业务流水号初始化短信配置
        /// </summary>
        /// <param name="BizNum"></param>
        /// <returns></returns>
        Task<List<SmsConfig>> InitSmsConfig(string BizNum);
        /// <summary>
        /// 更新或保存
        /// </summary>
        /// <param name="configs"></param>
        /// <returns></returns>
        Task<List<SmsConfig>> SaveOrUpdateSmsConfig(List<SmsConfig> configs);
    }
}
