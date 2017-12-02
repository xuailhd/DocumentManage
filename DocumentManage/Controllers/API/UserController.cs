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
        private readonly UserService userService;
        public UserController()
        {
            userService = new UserService();
        }

        [IgnoreUserAuthenticate]
        [HttpPost]
        public ApiResult Login([FromBody]RequestLoginDTO request)
        {
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

        [HttpPost]
        public ApiResult UpdatePassword([FromBody]RequestChangePasswordDTO request)
        {
            request.UserID = SecurityHelper.LoginUser.UserID;
            var ret = userService.UpdatePassword(request);

            if (ret)
            {
                return new ApiResult() { Status = EnumApiStatus.BizOK, Msg = "操作成功" };
            }
            else
            {
                return new ApiResult() { Status = EnumApiStatus.BizError, Msg = "旧密码错误" };
            }
        }

        [HttpPost]
        public ApiResult LoginOut()
        {
            var ret = userService.LoginOut(SecurityHelper.LoginUser.UserID);

            if (ret)
            {
                return new ApiResult() { Status = EnumApiStatus.BizOK };
            }
            else
            {
                return new ApiResult() { Status = EnumApiStatus.BizError };
            }
        }

        [HttpGet]
        public ApiResult GetUserInfo()
        {
            return userService.GetUserInfo(SecurityHelper.LoginUser.UserID).ToApiResult();
        }

        [HttpPost]
        public ApiResult UpdateUserInfo(RequestUserInfoDTO dto)
        {
            return userService.UpdateUserInfo(dto, SecurityHelper.LoginUser.UserID).ToApiResult();
        }
    }
}