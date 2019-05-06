
using TEAMModelOS.SDK.Extension.MessagePush.Interfaces;
using TEAMModelOS.SDK.Extension.MessagePush.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TEAMModelOS.SDK.Extension.Language.Interfaces;
using TEAMModelOS.SDK.Module.AzureTable.Interfaces;
using Microsoft.Extensions.Options;
using TEAMModelOS.SDK.Extension.Language.Model;
using TEAMModelOS.SDK.Helper.Common.JsonHelper;
using TEAMModelOS.SDK.Helper.Security.Md5Hash;
using TEAMModelOS.SDK.Helper.Network.HttpHelper;
using TEAMModelOS.SDK.Extension.HttpClient.Implements;

namespace TEAMModelOS.SDK.Extension.MessagePush.Implements
{
    public class SendCloudService : ISendCloudService
    {
        public SmsSendCloud smsSendCloud;
        public ILanguageService _languageService;
        public IAzureTableDBRepository _azureTableDBRepository;
        public HttpClientSendCloud _httpClientService;
        public SendCloudService(IOptions<SmsSendCloud> _option, ILanguageService languageService , IAzureTableDBRepository azureTableDBRepository , HttpClientSendCloud httpClientService )
        {
            _azureTableDBRepository = azureTableDBRepository;
            _languageService = languageService;
            smsSendCloud = _option.Value;
            _httpClientService = httpClientService;
        }


        public List<Dictionary<string, string>> Languages { get; set; } = new List<Dictionary<string, string>>
        {
            new Dictionary<string, string> { { "name", "简体中文" }, { "code","CHS"  } },
            new Dictionary<string, string> { { "name", "繁体中文" }, { "code","CHT" } },
            new Dictionary<string, string> { { "name", "英语" }, { "code","EN"  } } };
        public List<Dictionary<string, string>> BizCodes { get; set; } = new List<Dictionary<string, string>>
        {
            //new Dictionary<string, string> { { "name", "验证码业务" }, { "code", "Captcha" } },
            new Dictionary<string, string> { { "name", "报名通知业务" }, { "code", "SignUp" } }
        };
        public async Task<List<SmsConfig>> SaveOrUpdateSmsConfig(List<SmsConfig> configs) {
            return await _azureTableDBRepository.SaveOrUpdateAll<SmsConfig>(configs); 
        }
        public async Task<List<SmsConfig>> InitSmsConfig(string BizNum)
        {
            List<SmsConfig> smsConfigs = new List<SmsConfig>() ;
            List<SmsConfig> SaveSmsConfigs = new List<SmsConfig>();
            foreach (Dictionary<string, string> BizCode in BizCodes) {
                foreach (Dictionary<string, string> Language in Languages) {
                    Dictionary<string, object> dict = new Dictionary<string, object>
                    {
                        { "BizNum", BizNum },{ "BizCode", BizCode["code"] }, { "Language", Language["code"] } ,
                    };
                    SmsConfig smsConfig = await _azureTableDBRepository.FindOneByDict<SmsConfig>(dict);

                    if (smsConfig != null && smsConfig.RowKey != null)
                    {
                        smsConfigs.Add(smsConfig);
                    }
                    else {
                        DateTimeOffset now = DateTimeOffset.Now;
                        SmsConfig SaveSmsConfig = new SmsConfig {
                            RowKey = BizNum + "-" + BizCode["code"] + "-" + Language["code"],
                            PartitionKey = BizNum,
                            Language = Language["code"],
                            LanguageName = Language["name"],
                            BizCode = BizCode["code"],
                            BizName = BizCode["name"],
                            BizNum = BizNum,
                            Template = null,
                            Timestamp= now
                        };
                        SaveSmsConfigs.Add(SaveSmsConfig);
                        smsConfigs.Add(SaveSmsConfig);
                    }
                }
            }
            if (SaveSmsConfigs.Count > 0) {
               await _azureTableDBRepository.SaveOrUpdateAll<SmsConfig>(SaveSmsConfigs);
            }
            return smsConfigs; 
        }
        /// <summary>
        /// https://sendcloud.kf5.com/posts/view/1074678/
        /// </summary>
        /// <param name="BizCode"></param>
        /// <param name="CountryCode"></param>
        /// <param name="phone"></param>
        /// <param name="vars"></param>
        /// <returns></returns>
        public async Task<SendCloudResponse> SendSmsByBizCode(string  BizNum  , string BizCode, int CountryCode, string phone, Dictionary<string, string> vars = null)
        {
            SmsCountryCode code = null;
            bool flag = _languageService.GetSmsLanguage().TryGetValue(CountryCode + "", out code);
            string SmsLang = "EN";
            int templateId = 0;
            if (flag)
            {
                SmsLang = code.SmsLang;
            }
            Dictionary<string, object> dict = new Dictionary<string, object>
            {
                { "BizNum", BizNum },{ "BizCode", BizCode }, { "Language", SmsLang } ,
            };
            SmsConfig smsConfig   =  await _azureTableDBRepository.FindOneByDict<SmsConfig>(dict);

            if (smsConfig != null && smsConfig.RowKey != null )
            {
                //默认调用英文，不管是否包含，发送时会去处理相关信息
                int.TryParse(smsConfig.Template ,out templateId);
            }
            //else {
            //    throw new BizException("");
            //   //  templateId = smsSendCloud.SmsCode[code.SmsLang];
            //}
            int msgType = 0;
            if (CountryCode != 86)
            {
                msgType = 2;
                return await SendSms(templateId, "00" + CountryCode + phone, vars, msgType);
            }
            else
            {
                return await SendSms(templateId, phone, vars, msgType);
            }
            
        }
        public async Task<SendCloudResponse> SendSms(int templateId, string phone, Dictionary<string, string> vars = null, int msgType = 0)
        {
            List<KeyValuePair<string, string>> paramList = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("msgType", msgType+""),//0表示短信, 1表示彩信,2表示国际短信， 默认值为0
                new KeyValuePair<string, string>("phone", phone),
                new KeyValuePair<string, string>("smsUser", smsSendCloud.SmsUser),
                new KeyValuePair<string, string>("templateId", templateId+""),
            };
            if (vars != null)
            {
                paramList.Add(new KeyValuePair<string, string>("vars", MessagePackHelper.ObjectToJson(vars)));
            }
            var param_str = "";
            foreach (var param in paramList)
            {
                param_str += param.Key.ToString() + "=" + param.Value.ToString() + "&";
            }
            string sign_str = smsSendCloud.SmsKey + "&" + param_str + smsSendCloud.SmsKey;
            string sign = Md5Hash.Encrypt(sign_str);
            paramList.Add(new KeyValuePair<string, string>("signature", sign));
            string result = await _httpClientService.HttpPostAsync(smsSendCloud.SmsUrl, paramList);
            return MessagePackHelper.JsonToObject<SendCloudResponse>(result);
        }
    }
}
