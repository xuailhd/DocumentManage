using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DocumentManage.EF;
using DocumentManage.Models;

namespace DocumentManage
{
    public static class SecurityHelper
    {
        static string UserToken
        {
            get
            {
                if (HttpContext.Current != null)
                {
                    var userTokenStr = HttpContext.Current.Request.Headers["usertoken"];

                    if (string.IsNullOrWhiteSpace(userTokenStr))
                    {
                        //获取Cookie
                        HttpCookie authCookie = HttpContext.Current.Request.Cookies["usertoken"];

                        if (authCookie != null)
                        {
                            return authCookie.Value;
                        }
                        else
                        {
                            authCookie = HttpContext.Current.Request.Cookies[$"userToken"];

                            if (authCookie != null)
                            {
                                return authCookie.Value;
                            }
                            else
                            {
                                return "";
                            }
                        }
                    }
                    else
                    {
                        return userTokenStr;
                    }
                }
                else
                {
                    return "";
                }
            }
        }

        public static User IsLogin()
        {
            if (string.IsNullOrEmpty(UserToken))
            {
                return null;
            }
            using(var db = new DBEntities())
            {
                var userToken = UserToken;
                var model = db.Users.Where(t => t.UserToken == userToken).FirstOrDefault();

                return model;
            }
        }

        public static User LoginUser
        {
            get
            {
                return IsLogin();
            }
        }
    }
}