using MessagePack;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;
using TEAMModelOS.SDK.Context.Constant.Common;
using TEAMModelOS.SDK.Context.Exception;
using TEAMModelOS.SDK.Helper.Common.JsonHelper;

namespace TEAMModelOS.SDK.Context.Filter
{
    public class HttpGlobalExceptionInvoke
    {
        private readonly RequestDelegate _next;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="next"></param>
        public HttpGlobalExceptionInvoke(RequestDelegate next)
        {
            _next = next;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {

            System.Exception exs = null;
            bool isCatched = false;
            try
            {
                await _next(context);
            }
            catch (System.Exception ex) //发生异常
            {
                exs = ex;
                //自定义业务异常
                if (ex is BizException)
                {
                    context.Response.StatusCode = ((BizException)ex).code;
                }
                //未知异常
                else
                {
                    context.Response.StatusCode = 500;
                    //LogHelper.SetLog(LogLevel.Error, ex);
                }
                await HandleExceptionAsync(context, context.Response.StatusCode, ex.Message, ex.StackTrace);
                isCatched = true;
            }
            finally
            {
                if (!isCatched && context.Response.StatusCode != 200)//未捕捉过并且状态码不为200
                {
                    string msg = "";
                    switch (context.Response.StatusCode)
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
                            msg = exs.Message;
                            break;
                        case 403:
                            msg = exs.Message;
                            break;
                        default:
                            msg = "Unknown Error";
                            break;
                    }
                    await HandleExceptionAsync(context, context.Response.StatusCode, msg, exs == null ? "" : exs.StackTrace);
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// 
        /// 
        /// <param name="statusCode"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        private static async Task HandleExceptionAsync(HttpContext context, int statusCode, string msg, string devmsg)
        {
            var data = new ErrorResponse<string>(statusCode, msg, devmsg);
            context.Response.ContentType = Constants.CONTENT_TYPE_JSON;
            await context.Response.WriteAsync(MessagePackHelper.ObjectToJson(data));
        }
        /// <summary>
        /// 异常信息封装
        /// </summary>
        /// 
        [MessagePackObject(keyAsPropertyName: true)]
        public class ErrorResponse<T>
        {
            public ErrorResponse()
            {
                error = new ErrorModel<T>();
            }
            public ErrorResponse(string message)
            {
                error = new ErrorModel<T>
                {
                    message = message,
                    devmsg = message
                };
            }
            public ErrorResponse(int code, string message)
            {
                error = new ErrorModel<T>
                {
                    message = message,
                    devmsg = message,
                    code = code
                };

            }
            public ErrorResponse(int code, string message, string devMessage)
            {
                error = new ErrorModel<T>
                {
                    message = message,
                    devmsg = message,
                    code = code
                };
                error.devmsg = devMessage;
            }

            public string jsonrpc { get; set; } = "2.0";
            public double id { get; set; } = 1;
            private object result { get; set; } = null;
            public ErrorModel<T> error { get; set; } = null;
        }
    }
    [MessagePackObject(keyAsPropertyName: true)]
    public class ErrorModel<E>
    {
        public long responseTime = DateTime.Now.Ticks;
        public float code { get; set; } = 1;
        public string message { get; set; }
        public string devmsg { get; set; }
        public E data { get; set; }
    }
}
