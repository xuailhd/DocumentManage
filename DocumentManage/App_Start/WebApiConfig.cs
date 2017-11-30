using DocumentManage.Filters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;

namespace DocumentManage
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {

            #region Web API配置路由

            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index",id = RouteParameter.Optional }
            );
            #endregion

            #region 设置过滤器
            //统一权限验证
            config.Filters.Add(new ApiAuthorizationFilterAttribute());

            #endregion

            #region 设置api的返回结果类型

            config.Formatters.Remove(config.Formatters.XmlFormatter);

            //默认返回 json  
            config.Formatters.JsonFormatter.MediaTypeMappings.Add(
                new QueryStringMapping("datatype", "json", "application/json"));

            //json 序列化设置  
            config.Formatters.JsonFormatter.SerializerSettings =
                new JsonSerializerSettings()
                {
                    NullValueHandling = NullValueHandling.Ignore, //设置忽略值为 null 的属性  
                    //DateTimeZoneHandling = DateTimeZoneHandling.Local,  //DateTime默认为本地时区
                    //DateFormatString = "yyyy-MM-dd HH:mm:ss"
                };

            #endregion

        }
    }
}
