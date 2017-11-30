using DocumentManage.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace DocumentManage
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            GlobalFilters.Filters.Add(new HandleAndLogErrorAttribute());
            GlobalFilters.Filters.Add(new RoleAuthorizeAttribute());
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            //指定HTTP 请求内容的类型 2016年8月31日 郭明添加(云通信回调时需要)
            Request.Headers.Add("Content-Type", "application/json; charset=utf-8");

            #region  Cors跨域设置
            //Cors跨域设置(这些配制放在HttpMethod=="OPTIONS"里面就调用出错，放出来就没事，不知原因)
            var response = HttpContext.Current.Response;

            response.AddHeader("Access-Control-Allow-Origin", "*"); //正式环境注意改成具体网站，*代表允许所有网站
            response.AddHeader("Access-Control-Allow-Methods", "GET,POST,PUT,DELETE,OPTIONS");
            response.AddHeader("Access-Control-Allow-Headers", "Content-Type,X-Requested-With,x-apptoken,x-noncestr,x-usertoken,x-sign,apptoken,noncestr,usertoken,sign");//Content-Type
            response.AddHeader("Access-Control-Max-Age", "36000");//设置跨域缓存，减少浏览器OPTIONS访问次数

            if (HttpContext.Current.Request.HttpMethod == "OPTIONS")
            {
                response.End();
            }

            #endregion

        }
    }
}
