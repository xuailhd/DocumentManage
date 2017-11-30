using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DocumentManage.Dtos
{
    public static class ApiResultExtend
    {
        public static ApiResult ToApiResult<TEntity>(this PagedList<TEntity> pageList, EnumApiStatus status = EnumApiStatus.BizOK, string msg = "操作成功")
        {
            return new ApiResult
            {
                Data = pageList,
                Total = pageList.TotalCount,
                Msg = msg,
                Status = status
            };
        }

        public static ApiResult ToApiResult(this object obj, EnumApiStatus status = EnumApiStatus.BizOK, string msg = "操作成功")
        {
            return new ApiResult
            {
                Data = obj,
                Msg = msg,
                Status = status
            };
        }

        public static ApiResult ToApiFailed(this object obj, EnumApiStatus status = EnumApiStatus.BizError, string msg = "操作失败")
        {
            return new ApiResult
            {
                Data = obj,
                Msg = msg,
                Status = status
            };
        }

        public static ApiResult ToApiSucceed(this object obj, string msg = "操作成功")
        {
            return new ApiResult(obj)
            {
                Data = obj,
                Msg = msg,
                Status = EnumApiStatus.BizOK
            };
        }
    }
}