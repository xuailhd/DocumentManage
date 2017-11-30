using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace DocumentManage.Filters
{
    public class RoleAuthorizeAttribute : AuthorizeAttribute
    {
        private Type _type;
        private ActionDescriptor _actionDescriptor;
        public override void OnAuthorization(AuthorizationContext filterContext)
        {

            _type = filterContext.Controller.GetType();
            _actionDescriptor = filterContext.ActionDescriptor;

            base.OnAuthorization(filterContext);
        }   

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException("httpContext");
            }

            //未登录则需要进行授权
            if (SecurityHelper.IsLogin() == null)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 处理未授权的请求
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            //没有忽略验证的则进行验证
            if (!IsIgnoreAuthenticate(filterContext))
            {
                filterContext.Result = new RedirectResult("~/Login");
            }

        }


        /// <summary>
        /// 是否忽略接入API认证
        /// </summary>
        /// <param name="actionContext"></param>
        /// <returns></returns>
        private bool IsIgnoreAuthenticate(AuthorizationContext actionContext)
        {
            var actionIgnoreAuthcate = GetActionOrControllerAttributes(actionContext,typeof(IgnoreUserAuthenticateAttribute));
            if (actionIgnoreAuthcate == null || actionIgnoreAuthcate.Length <= 0)
                return false;

            return true;
        }


        /// <summary>
        /// 获取action或Controller的特性
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="actionContext"></param>
        /// <returns></returns>
        private object[] GetActionOrControllerAttributes(AuthorizationContext actionContext,Type type)            
        {
            var actionIgnoreAuthcate = actionContext.ActionDescriptor.ControllerDescriptor.GetCustomAttributes(type, true);
            if (actionIgnoreAuthcate == null || actionIgnoreAuthcate.Length <= 0)
                actionIgnoreAuthcate = actionContext.ActionDescriptor.GetCustomAttributes(type, true);
            return actionIgnoreAuthcate;
        }
    }
}