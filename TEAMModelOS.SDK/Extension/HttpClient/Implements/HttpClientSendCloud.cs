using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TEAMModelOS.SDK.Context.Configuration;
using TEAMModelOS.SDK.Context.Constant.Common;
using HttpClientSpace = System.Net.Http;

namespace TEAMModelOS.SDK.Extension.HttpClient.Implements
{
    /// <summary>
    /// 需要调整的  https://blog.yowko.com/httpclientfactory-dotnet-core-dotnet-framework/
    /// </summary>
    public class HttpClientSendCloud
    {
        HttpClientSpace.HttpClient client { get; }
        public HttpClientSendCloud(HttpClientSpace.HttpClient _client)
        {
           // _client.DefaultRequestHeaders.Add(Constants.AUTHORIZATION, BaseConfigModel.Configuration["SmsSendCloud:UserInfoKey"]);
            client = _client;
        }

        /// <summary>
        /// 同步GET请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="headers"></param>
        /// <param name="timeout">请求响应超时时间，单位/s(默认100秒)</param>
        /// <returns></returns>
        public string HttpGet(string url, Dictionary<string, string> headers = null, int timeout = 0)
        {

            //if (headers != null)
            //{
            //    foreach (KeyValuePair<string, string> header in headers)
            //    {
            //        client.DefaultRequestHeaders.Add(header.Key, header.Value);
            //    }
            //}
            if (timeout > 0)
            {
                client.Timeout = new TimeSpan(0, 0, timeout);
            }
            byte[] resultBytes = client.GetByteArrayAsync(url).Result;
            return Encoding.UTF8.GetString(resultBytes);
        }

        /// <summary>
        /// 异步GET请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="headers"></param>
        /// <param name="timeout">请求响应超时时间，单位/s(默认100秒)</param>
        /// <returns></returns>
        public async Task<string> HttpGetAsync(string url, Dictionary<string, string> headers = null, int timeout = 0)
        {
            //if (headers != null)
            //{
            //    foreach (KeyValuePair<string, string> header in headers)
            //    {
            //        client.DefaultRequestHeaders.Add(header.Key, header.Value);
            //    }
            //}
            if (timeout > 0)
            {
                client.Timeout = new TimeSpan(0, 0, timeout);
            }
            byte[] resultBytes = await client.GetByteArrayAsync(url);
            return Encoding.Default.GetString(resultBytes);
        }


        /// <summary>
        /// 同步POST请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="postData"></param>
        /// <param name="headers"></param>
        /// <param name="contentType"></param>
        /// <param name="timeout">请求响应超时时间，单位/s(默认100秒)</param>
        /// <param name="encoding">默认UTF8</param>
        /// <returns></returns>
        public string HttpPost(string url, string postData, Dictionary<string, string> headers = null, string contentType = null, int timeout = 0, Encoding encoding = null)
        {
            //if (headers != null)
            //{
            //    foreach (KeyValuePair<string, string> header in headers)
            //    {
            //        client.DefaultRequestHeaders.Add(header.Key, header.Value);
            //    }
            //}
            if (timeout > 0)
            {
                client.Timeout = new TimeSpan(0, 0, timeout);
            }
            HttpContent content = new StringContent(postData ?? "", encoding ?? Encoding.UTF8);

            if (contentType != null)
            {
                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(contentType);
            }
            HttpResponseMessage responseMessage = client.PostAsync(url, content).Result;
            byte[] resultBytes = responseMessage.Content.ReadAsByteArrayAsync().Result;
            return Encoding.UTF8.GetString(resultBytes);
        }

        /// <summary>
        /// 异步POST请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="postData"></param>
        /// <param name="headers"></param>
        /// <param name="contentType"></param>
        /// <param name="timeout">请求响应超时时间，单位/s(默认100秒)</param>
        /// <param name="encoding">默认UTF8</param>
        /// <returns></returns>
        public async Task<string> HttpPostAsync(string url, string postData, Dictionary<string, string> headers = null, string contentType = null, int timeout = 0, Encoding encoding = null)
        {
            //if (headers != null)
            //{
            //    foreach (KeyValuePair<string, string> header in headers)
            //    {
            //        client.DefaultRequestHeaders.Add(header.Key, header.Value);
            //    }
            //}
            if (timeout > 0)
            {
                client.Timeout = new TimeSpan(0, 0, timeout);
            }
            HttpContent content = new StringContent(postData ?? "", encoding ?? Encoding.UTF8);

            if (contentType != null)
            {
                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(contentType);
            }
            HttpResponseMessage responseMessage = await client.PostAsync(url, content);
            byte[] resultBytes = await responseMessage.Content.ReadAsByteArrayAsync();
            return Encoding.UTF8.GetString(resultBytes);
        }

        /// <summary>
        /// 异步POST请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="postData"></param>
        /// <param name="headers"></param>
        /// <param name="contentType"></param>
        /// <param name="timeout">请求响应超时时间，单位/s(默认100秒)</param>
        /// <param name="encoding">默认UTF8</param>
        /// <returns></returns>
        public async Task<string> HttpPostAsync(string url, List<KeyValuePair<string, string>> postData, Dictionary<string, string> headers = null, string contentType = null, int timeout = 0, Encoding encoding = null)
        {
            //if (headers != null)
            //{
            //    foreach (KeyValuePair<string, string> header in headers)
            //    {
            //        client.DefaultRequestHeaders.Add(header.Key, header.Value);
            //    }
            //}
            if (timeout > 0)
            {
                client.Timeout = new TimeSpan(0, 0, timeout);
            }
            HttpContent content = new FormUrlEncodedContent(postData);
            if (contentType != null)
            {
                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(contentType);
            }
            HttpResponseMessage responseMessage = await client.PostAsync(url, content);
            byte[] resultBytes = await responseMessage.Content.ReadAsByteArrayAsync();
            return Encoding.UTF8.GetString(resultBytes);
        }
    }
}
