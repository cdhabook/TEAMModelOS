using TEAMModelOS.SDK.Extension.JwtAuth.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace TEAMModelOS.SDK.Extension.JwtAuth.Filters
{
    public class JwtAuthorizationFilter
    {
        private readonly RequestDelegate _next;
        public JwtAuthorizationFilter(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public Task Invoke(HttpContext httpContext)
        {
            //检测是否包含'Authorization'请求头，如果不包含则直接放行
            if (!httpContext.Request.Headers.ContainsKey(HttpConstant.Authorization) &&!httpContext.Request.Query.ContainsKey(HttpConstant.access_token))
            {
                return _next(httpContext);
            }
            var tokenHeader = "";
            if (httpContext.Request.Headers.ContainsKey(HttpConstant.Authorization))
            {
                tokenHeader = httpContext.Request.Headers[HttpConstant.Authorization];
                tokenHeader = tokenHeader.Replace("Bearer ", "");
            }
            if (httpContext.Request.Query.ContainsKey(HttpConstant.access_token))
            {
                tokenHeader = httpContext.Request.Query[HttpConstant.access_token];
                tokenHeader = tokenHeader.Trim();
            }
            ClaimModel claimModel = JwtHelper.JwtHelper.SerializeJWT(tokenHeader);

            //将tokenModel存入缓存中
            //授权
            //已经弃用该方式获取User信息，采用官方认证，必须在上边ConfigureService 中，配置JWT的认证服务 (.AddAuthentication 和 .AddJwtBearer 二者缺一不可)
            //var identity = new ClaimsIdentity(claimModel.Claims);
            //var principal = new ClaimsPrincipal(identity);
            //httpContext.User = principal;

            return _next(httpContext);
        }
    }
}
