using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using DocumentManage.Dtos;

namespace DocumentManage.Filters
{

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class ApiAuthorizationFilterAttribute : AuthorizeAttribute//, AuthorizationFilterAttribute
    {

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            var req = HttpContext.Current.Request;
            var userToken = getRequestParam("usertoken");
            var encryptUserId = getRequestParam("userid");

            if (!IsIgnoreUserAuthenticate(actionContext))
            {
                var model = SecurityHelper.IsLogin();
                if (model == null)
                {
                    var result = new ApiMessageResult() { Status = EnumApiStatus.ApiUserNotLogin, Msg = "用户未登录" };
                    actionContext.Response = new HttpResponseMessage() { Content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(result), Encoding.UTF8, "application/json") };
                    return;
                }
            }
        }

        
        public string getRequestParam(string paramName)
        {
            var req = HttpContext.Current.Request;
            return req.Headers[paramName] ?? req.QueryString["x-" + paramName];
        }

     

        /// <summary>
        /// 是否忽略用户登录认证
        /// </summary>
        /// <param name="actionContext"></param>
        /// <returns></returns>
        private bool IsIgnoreUserAuthenticate(HttpActionContext actionContext)
        {
            var actionIgnoreAuthcate = GetActionOrControllerAttributes<IgnoreUserAuthenticateAttribute>(actionContext);
            if (actionIgnoreAuthcate == null || actionIgnoreAuthcate.Count <= 0)
                return false;

            return true;
        }


        /// <summary>
        /// 获取action或Controller的特性
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="actionContext"></param>
        /// <returns></returns>
        private Collection<T> GetActionOrControllerAttributes<T>(HttpActionContext actionContext)
            where T : class
        {
            var actionIgnoreAuthcate = actionContext.ActionDescriptor.ControllerDescriptor.GetCustomAttributes<T>();
            if (actionIgnoreAuthcate == null || actionIgnoreAuthcate.Count <= 0)
                actionIgnoreAuthcate = actionContext.ActionDescriptor.GetCustomAttributes<T>();
            return actionIgnoreAuthcate;
        }
    }
}