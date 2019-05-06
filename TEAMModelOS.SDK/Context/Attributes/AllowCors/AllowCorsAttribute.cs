using Microsoft.AspNetCore.Mvc.Filters;

namespace TEAMModelOS.SDK.Context.Attributes.AllowCors
{
    /// <summary>
    /// 跨域处理
    /// </summary>
    public class AllowCorsAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var context = filterContext.HttpContext;
            //context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
            context.Response.Headers.Add("Access-Control-Allow-Methods", "GET, HEAD, OPTIONS, POST, PUT");
            context.Response.Headers.Add("Access-Control-Allow-Headers", "Access-Control-Allow-Headers," +
                                         " Origin,Accept, X-Requested-With, Content-Type, " +
                                         "Access-Control-Request-Method, Access-Control-Request-Headers," +
                                         "Content-Type,Accept,access_token,token,Authorization");
            base.OnActionExecuting(filterContext);
        }
    }
}
