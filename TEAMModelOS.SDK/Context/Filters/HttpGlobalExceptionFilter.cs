using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TEAMModelOS.SDK.Context.Exception;
using TEAMModelOS.SDK.Helper.Common.JsonHelper;
using static TEAMModelOS.SDK.Context.Filter.HttpGlobalExceptionInvoke;

namespace TEAMModelOS.SDK.Context.Filters
{
    /// <summary>
    /// 全局异常处理
    /// </summary>
    public class HttpGlobalExceptionFilter : IExceptionFilter
    {
        readonly ILoggerFactory _loggerFactory;
        readonly IHostingEnvironment _env;
        public HttpGlobalExceptionFilter(ILoggerFactory loggerFactory, IHostingEnvironment env)
        {
            _loggerFactory = loggerFactory;
            _env = env;
        }
        /// <summary>
        /// 异常拦截
        /// </summary>
        /// <param name="context"></param>
        public async void OnException(ExceptionContext context)
        {
            int code = context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            if (context.Exception is BizException)
            {
                context.HttpContext.Response.StatusCode = 200;
            }
            var errorResponse = new ErrorResponse<string>(1, context.Exception.Message);
            // errorResponse.devMessage = context.Exception.StackTrace;
            context.Result = new ApplicationErrorResult(errorResponse, code);
            context.ExceptionHandled = true;
            if (context.HttpContext.Response.StatusCode != 200)//未捕捉过并且状态码不为200
            {
                string msg = "";
                switch (context.HttpContext.Response.StatusCode)
                {
                    case 401:
                        msg = "Unauthorized";
                        break;
                    case 404:
                        msg = "Service Not Found";
                        break;
                    case 502:
                        msg = "Request Erro";
                        break;
                    case 500:
                        msg = context.Exception.Message;
                        break;
                    case 403:
                        msg = context.Exception.Message;
                        break;
                    default:
                        msg = "Unknown Error";
                        break;
                }
                await HandleExceptionAsync(context.HttpContext, context.HttpContext.Response.StatusCode, msg, context.Exception == null ? "" : context.Exception.StackTrace);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, int statusCode, string msg, string devmsg)
        {
            var data = new ErrorResponse<string>(statusCode, msg, devmsg);
            context.Response.ContentType = "application/json;charset=utf-8";
            await context.Response.WriteAsync(MessagePackHelper.ObjectToJson(data));
        }
    }
    public class ApplicationErrorResult : ObjectResult
    {
        public ApplicationErrorResult(object value, int code) : base(value)
        {
            StatusCode = code;
        }
    }
}
