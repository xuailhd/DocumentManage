using DocumentManage.Dtos;
using DocumentManage.Filters;
using DocumentManage.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace DocumentManage.Controllers.API
{
    public class UserController : ApiController
    {
        [IgnoreUserAuthenticate]
        [HttpPost]
        public ApiResult Login([FromBody]RequestLoginDTO request)
        {
            UserService userService = new UserService();
            var ret = userService.Login(request);

            if(ret == null)
            {
                return new ApiResult() {  Status = EnumApiStatus.BizError, Msg= "账号或密码错误"};
            }
            else
            {
                return ret.ToApiResult();
            }

        }

        [HttpGet]
        public ApiResult GetWaterNo([FromUri]int type)
        {
            return DocumentManage.Common.GloabSeq.GetWaterNo(type).ToApiResult();
        }
        
    }
}