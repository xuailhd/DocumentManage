using DocumentManage.Dtos;
using DocumentManage.Filters;
using DocumentManage.Models;
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

            if (ret == null)
            {
                return new ApiResult() { Status = EnumApiStatus.BizError, Msg = "账号或密码错误" };
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
            request.ID = SecurityHelper.LoginUser.ID;
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
        public ApiResult ResetPassword([FromBody]RequestChangePasswordDTO request)
        {
            var ret = userService.ResetPassword(request);

            if (ret)
            {
                return new ApiResult() { Status = EnumApiStatus.BizOK, Msg = "操作成功" };
            }
            else
            {
                return new ApiResult() { Status = EnumApiStatus.BizError, Msg = "修改失败" };
            }
        }

        [HttpPost]
        public ApiResult LoginOut()
        {
            var ret = userService.LoginOut(SecurityHelper.LoginUser.ID);

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
            return userService.GetUserInfo(SecurityHelper.LoginUser.ID).ToApiResult();
        }

        [HttpPost]
        public ApiResult UpdateUserInfo(RequestUserInfoDTO dto)
        {
            return userService.UpdateUserInfo(dto, SecurityHelper.LoginUser.ID).ToApiResult();
        }

        [HttpPost]
        public ApiResult EditRole([FromBody]RequestRoleDTO dto)
        {
            string reason = "";
            if (userService.EditRole(dto, out reason))
            {
                return new ApiResult();
            }
            else
            {
                return new ApiResult() { Status = EnumApiStatus.BizError, Msg = reason };
            }
        }

        [HttpPost]
        public ApiResult DeleteRole([FromBody]RequestRoleDTO dto)
        {
            string reason = "";
            if (userService.DeleteRole(dto, out reason))
            {
                return new ApiResult();
            }
            else
            {
                return new ApiResult() { Status = EnumApiStatus.BizError, Msg = reason };
            }
        }

        [HttpPost]
        public ApiResult GetRoleList([FromBody]RequestRoleQDTO request)
        {
            return userService.GetRoleList(request).ToApiResult();
        }

        [HttpPost]
        public ApiResult EditUserRoles([FromBody]RequestEditUserRoleDTO request)
        {
            string reason = "";
            if (userService.EditUserRoles(request, out reason))
            {
                return new ApiResult();
            }
            else
            {
                return new ApiResult() { Status = EnumApiStatus.BizError, Msg = reason };
            }
        }

        [HttpPost]
        public ApiResult EditRoleAuths([FromBody]RequestEditRoleAuthDTO request)
        {
            string reason = "";
            if (userService.EditRoleAuths(request, out reason))
            {
                return new ApiResult();
            }
            else
            {
                return new ApiResult() { Status = EnumApiStatus.BizError, Msg = reason };
            }
        }

        [HttpPost]
        public ApiResult GetAuthList([FromBody]RequestAuthModelQDTO request)
        {
            return userService.GetAuthList(request).ToApiResult();
        }

        [HttpPost]
        public ApiResult GetUserList([FromBody]RequestUserQDTO request)
        {
            return userService.GetUserList(request).ToApiResult();
        }

        [HttpPost]
        public ApiResult AddAccount([FromBody]RequestUserInfoDTO request)
        {
            string reason = "";
            if (userService.AddAccount(request, out reason))
            {
                return new ApiResult();
            }
            else
            {
                return new ApiResult() { Status = EnumApiStatus.BizError, Msg = reason };
            }
        }
        [HttpPost]
        public ApiResult DeleteAccount([FromBody]RequestUserInfoDTO request)
        {
            string reason = "";
            if (userService.DeleteAccount(request, out reason))
            {
                return new ApiResult();
            }
            else
            {
                return new ApiResult() { Status = EnumApiStatus.BizError, Msg = reason };
            }
        }
    }
}