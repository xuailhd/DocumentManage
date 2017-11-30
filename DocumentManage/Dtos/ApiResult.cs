using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace DocumentManage.Dtos
{

    [Description("WebAPI返回状态定义")]
    public enum EnumApiStatus
    {
        #region 默认业务状态 0~1

        [Description("操作成功")]
        BizOK = 0,

        /// <summary>
        /// 操作失败
        /// </summary>
        [Description("操作失败")]
        BizError = 1,

        #endregion

        #region 系统接口状态 2~99

        /// <summary>
        /// 接口参数签名错误
        /// </summary>     
        [Description("接口签名参数错误")]
        ApiParamSignError = 2,

        /// <summary>
        /// 非法请求
        /// </summary>        
        [Description("接口用户令牌错误")]
        ApiParamTokenError = 3,

        /// <summary>
        /// 接口参数数据验证失败
        /// </summary>
        [Description("接口数据验证失败")]
        ApiParamModelValidateError = 4,

        /// <summary>
        /// 接口参数应用签名过期
        /// </summary>
        [Description("接口应用令牌过期")]
        ApiParamAppTokenExpire = 5,

        /// <summary>
        /// 接口时间戳参数错误
        /// </summary>
        [Description("接口时间戳参数错误")]
        ApiParamTimestampError = 9,

        /// <summary>
        /// 重复请求
        /// </summary>
        [Description("接口随机参数错误（重复请求)")]
        ApiRepeatedAccess = 8,

        /// <summary>
        /// 用户未登录
        /// </summary>
        [Description("用户未登录")]
        ApiUserNotLogin = 6,

        /// <summary>
        /// 用户无权限访问
        /// </summary>
        [Description("用户无权限访问")]
        ApiUserUnauthorized = 7,

        /// <summary>
        /// 操作成功
        /// </summary>      

        #endregion
    }

    /// <summary>
    ///  API返回单个实体类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ApiResult : ApiMessageResult
    {
        public ApiResult()
            : base(EnumApiStatus.BizOK, "操作成功") { }

        public ApiResult(object data)
            : base(EnumApiStatus.BizOK, "操作成功")
        {
            this.Data = data;
        }

        public ApiResult(object data, EnumApiStatus status, string msg)
            : base(status, msg)
        {
            this.Data = data;
        }

        public ApiResult(Exception ex)
            : base(ex)
        {

        }

        /// <summary>
        /// 数据
        /// </summary>
        public object Data { get; set; }

        public int? Total { get; set; }
    }


    /// <summary>
    /// API返回消息基类
    /// </summary>
    public class ApiMessageResult
    {
        public ApiMessageResult() { }
        public ApiMessageResult(EnumApiStatus status, string msg)
        {
            this.Status = status;
            this.Msg = msg;
        }
        public ApiMessageResult(Exception ex)
        {
            this.Status = EnumApiStatus.BizError;
            this.Msg = "操作失败：" + ex.Message; //ex.GetDetailException();
        }

        public EnumApiStatus Status { get; set; }

        /// <summary>
        /// 消息说明(对应Status的文本说明)
        /// </summary>
        public string Msg { get; set; }
    }



    public class ApiResultClient<T> : ApiMessageResult
    {
        /// <summary>
        /// 数据
        /// </summary>
        public T Data { get; set; }

        public int? Total { get; set; }
    }
}