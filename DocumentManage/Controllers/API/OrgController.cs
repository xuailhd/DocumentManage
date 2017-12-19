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
    public class OrgController : ApiController
    {
        [HttpPost]
        public ApiResult Edit([FromBody]Orgnazition request)
        {
            OrgService orgService = new OrgService();
            string reason;
            var ret = orgService.Edit(request, SecurityHelper.LoginUser.ID, out reason);

            if (!ret)
            {
                return new ApiResult() { Status = EnumApiStatus.BizError, Msg = reason };
            }
            else
            {
                return ret.ToApiResult();
            }

        }

        [HttpPost]
        public ApiResult GetDetail([FromBody]RequestOrgQDTO request)
        {
            OrgService orgService = new OrgService();
            var ret = orgService.GetDetail(request);

            if (ret == null)
            {
                return new ApiResult() { Status = EnumApiStatus.BizError, Msg = "数据不存在，或没有权限" };
            }
            else
            {
                return ret.ToApiResult();
            }
        }

        [HttpPost]
        public ApiResult GetList([FromBody]RequestOrgQDTO request)
        {
            OrgService orgService = new OrgService();
            var ret = orgService.GetList(request);
            return ret.ToApiResult();
        }

        [HttpPost]
        public ApiResult Delete([FromBody]RequestOrgQDTO request)
        {
            OrgService orgService = new OrgService();
            string reason;
            var ret = orgService.Delete(request, SecurityHelper.LoginUser.ID, out reason);

            if (!ret)
            {
                return new ApiResult() { Status = EnumApiStatus.BizError, Msg = reason };
            }
            else
            {
                return ret.ToApiResult();
            }
        }
    }
}