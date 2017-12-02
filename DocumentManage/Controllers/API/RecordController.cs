using DocumentManage.Dtos;
using DocumentManage.Dtos.Request;
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
    public class RecordController : ApiController
    {
        private readonly RecordService recordService;
        public RecordController()
        {
            recordService = new RecordService();
        }

        [HttpPost]
        public ApiResult Edit([FromBody]RequestVisitRecordDTO request)
        {
            string reason;
            var ret = recordService.Edit(request, SecurityHelper.LoginUser.UserID, out reason);

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
        public ApiResult GetDetail([FromBody]RequestVisitRecordQDTO request)
        {
            var ret = recordService.GetDetail(request);

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
        public ApiResult GetList([FromBody]RequestVisitRecordQDTO request)
        {
            var ret = recordService.GetList(request);
            return ret.ToApiResult();
        }

        [HttpPost]
        public ApiResult GetQueryList([FromBody]RequestVisitRecordQDTO request)
        {
            var ret = recordService.GetQueryList(request);
            ApiResult apiResult = new ApiResult() { Data = ret, Total = ret.VisitRecords == null ? 0 : ret.VisitRecords.TotalCount };
            return apiResult;
        }

        [HttpPost]
        public ApiResult Delete([FromBody]RequestVisitRecordQDTO request)
        {
            var ret = recordService.Delete(request);

            if (!ret)
            {
                return new ApiResult() { Status = EnumApiStatus.BizError, Msg = "数据不存在，或没有权限" };
            }
            else
            {
                return ret.ToApiResult();
            }
        }
    }
}