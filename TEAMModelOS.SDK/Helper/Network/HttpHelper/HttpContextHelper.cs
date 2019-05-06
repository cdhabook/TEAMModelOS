using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using TEAMModelOS.SDK.Context.Constant.Common;
using TEAMModelOS.SDK.Extension.JwtAuth.JwtHelper;
using TEAMModelOS.SDK.Extension.JwtAuth.Models;

namespace TEAMModelOS.SDK.Helper.Network.HttpHelper
{
    public static class HttpContextHelper
    {
        /// <summary>
        /// 设置本地cookie
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>  
        /// <param name="minutes">过期时长，单位：分钟</param>      
        public static void SetCookies(HttpResponse Response, string key, string value, int minutes = 30)
        {
            Response.Cookies.Append(key, value, new CookieOptions
            {
                Expires = DateTime.Now.AddMinutes(minutes)
            });
        }
        /// <summary>
        /// 删除指定的cookie
        /// </summary>
        /// <param name="key">键</param>
        public static void DeleteCookies(HttpContext httpContext, string key)
        {
            httpContext.Response.Cookies.Delete(key);
        }

        /// <summary>
        /// 在Http中获取值
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>返回对应的值</returns>
        public static string GetValueInHttp(HttpRequest Request, string key)
        {
            string aktoken = "";
            if (string.IsNullOrEmpty(aktoken))
            {
                //或者在头获取
                Request.Headers.TryGetValue(key, out StringValues akh);
                if (!string.IsNullOrEmpty(akh))
                {
                    aktoken = akh;
                }
            }
            if (string.IsNullOrEmpty(aktoken))
            {
                //其次在参数中获取
                Request.Query.TryGetValue(key, out StringValues ak);
                if (!string.IsNullOrEmpty(ak))
                {
                    aktoken = ak;
                }
            }
            //从cookie获取
            if (string.IsNullOrEmpty(aktoken))
            {
                Request.Cookies.TryGetValue(key, out string value);
                if (!string.IsNullOrEmpty(value))
                {
                    aktoken = value;
                }
            }
            if (string.IsNullOrEmpty(aktoken))
            {
                //在referer获取
                Request.Headers.TryGetValue("referer", out StringValues referer);
                string token = "";
                if (referer.Contains(key + "="))
                {
                    string[] pramas = referer[0].Substring(referer[0].IndexOf(key + "=")).Split("&");
                    int len = pramas.Count();
                    if (len > 0)
                    {
                        for (int i = 0; i < len; i++)
                        {
                            if (pramas[i].Contains(key))
                            {
                                token = pramas[i].Split("=")[1];
                                break;
                            }
                        }
                    }
                }
                if (!string.IsNullOrEmpty(token))
                {
                    aktoken = token;
                }
            }
            return aktoken;
        }
        public static List<string> GetLoginUser(IHttpContextAccessor httpContextAccessor ,string claimType) {
            var tokenHeader = "";
            HttpRequest request = httpContextAccessor.HttpContext.Request;
            if (request.Headers.ContainsKey(Constants.AUTHORIZATION))
            {
                tokenHeader = request.Headers[Constants.AUTHORIZATION];
                //tokenHeader = tokenHeader.ToString().Substring("Bearer ".Length).Trim();
                tokenHeader = tokenHeader.Replace("Bearer ", "");
            }
            if (request.Query.ContainsKey(Constants.ACCESS_TOKEN))
            {
                tokenHeader = request.Query[Constants.ACCESS_TOKEN];
                tokenHeader = tokenHeader.Trim();
            }
            if (string.IsNullOrEmpty(tokenHeader))
            {
                return null;
            }
            ClaimModel claimModel = JwtHelper.SerializeJWT(tokenHeader);
            claimModel.Claim.TryGetValue(claimType, out var claimValue);
            List<string> claimValues = new List<string>();
            foreach (Claim claim in claimModel.Claims)
            {
                if (claim.Type.Equals(claimType))
                {
                    claimValues.Add(claim.Value);
                }
            }
            return claimValues;
        }
    }
}
