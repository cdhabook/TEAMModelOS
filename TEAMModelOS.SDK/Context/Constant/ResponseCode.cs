using System;
using System.Collections.Generic;
using System.Text;

namespace TEAMModelOS.SDK.Context.Constant.Common
{

    /// <summary>
    /// 响应编码
    /// </summary>
    public static class ResponseCode
    {
        /// <summary>
        /// 代表HTTP正常请求
        /// </summary>
        public readonly static int HTTP_SUCCESS = 200;
        /// <summary>
        /// 一般常用代表成功
        /// </summary>
        public readonly static int SUCCESS = 0;
        /// <summary>
        /// 一般常用代表失败
        /// </summary>
        public readonly static int FAILED = 1;
        /// <summary>
        /// 数据为空或null
        /// </summary>
        public readonly static int DATA_EMPTY_NULL = 2;
        /// <summary>
        /// 参数异常
        /// </summary>
        public readonly static int PARAMS_ERROR = 3;
        /// <summary>
        /// 服务端异常
        /// </summary>
        public readonly static int SERVER_ERROR = 500;
        /// <summary>
        /// 未知异常
        /// </summary>
        public readonly static int UNDEFINED = 9;
        /// <summary>
        /// 资源未找到
        /// </summary>
        public readonly static int NOT_FOUND = 404;
        /// <summary>
        /// 未授权
        /// </summary>
        public readonly static int UNAUTH = 401;
        /// <summary>
        /// 网关路由异常
        /// </summary>
        public readonly static int GATEWAY_ERROR = 504;
        /// <summary>
        /// 响应超时
        /// </summary>
        public readonly static int TIMEOUT_ERROR = 503;
        
    }
}
