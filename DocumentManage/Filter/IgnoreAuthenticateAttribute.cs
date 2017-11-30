using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace DocumentManage.Filters
{

    /// <summary>
    /// 忽略用户登录验证特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class IgnoreUserAuthenticateAttribute : Attribute
    {

    }
}