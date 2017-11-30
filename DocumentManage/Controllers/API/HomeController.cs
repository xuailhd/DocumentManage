using DocumentManage.Dtos;
using DocumentManage.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace DocumentManage.Controllers.API
{
    public class HomeController : ApiController
    {
        [IgnoreUserAuthenticate]
        public ApiResult Index()
        {
            return "ok".ToApiResult();
        }

        
    }
}