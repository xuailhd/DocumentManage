using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace DocumentManage.Models
{
    public class VisitRecord : BaseModel
    {
        [Key]
        public string VisitID { get; set; }

        /// <summary>
        /// 访问类型
        /// </summary>
        public string VisitType { get; set; }

        /// <summary>
        /// 档案名称
        /// </summary>
        public string VisitName { get; set; }

        /// <summary>
        /// 访问目的
        /// </summary>
        public string VisitFor { get; set; }

        /// <summary>
        /// 主要经办部门
        /// </summary>
        public string MianDepartment { get; set; }

        public DateTime? FromDate { get; set; }
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// 访问标注
        /// </summary>
        public string VisitTag { get; set; }

        /// <summary>
        /// 访问性质
        /// </summary>
        public string VisType { get; set; }

        /// <summary>
        /// 费用承担
        /// </summary>
        public string FeeType { get; set; }

        /// <summary>
        /// 支付方式
        /// </summary>
        public string PayType { get; set; }

        /// <summary>
        /// 接待标准
        /// </summary>
        public string TakeLevel { get; set; }

        public string IsLine { get; set; }

        public string AnsLevel { get; set; }

        public string Remark { get; set; }
    }
}