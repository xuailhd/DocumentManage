using DocumentManage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DocumentManage.Dtos
{
    public class RequestVisitRecordDTO
    {
        public string VisitID { get; set; }

        public string VisitType { get; set; }

        public string VisitName { get; set; }

        public string VisitFor { get; set; }

        public string MainDepartment { get; set; }

        public DateTime? FromDate { get; set; }
        public DateTime? EndDate { get; set; }

        public List<string> VisitTags { get; set; }

        public string VisType { get; set; }

        public string FeeType { get; set; }

        public string PayType { get; set; }

        public string TakeLevel { get; set; }

        public string IsLine { get; set; }

        public string AnsLevel { get; set; }

        public string Remark { get; set; }

        public List<VisitDetail> VisitDetails { get; set; }

        /// <summary>
        /// 主要经办人
        /// </summary>
        public List<VisitPerson> MianPersons { get; set; }

        /// <summary>
        /// 接待机构（中方）
        /// </summary>
        public List<VisitOrg> OurOrgs { get; set; }

        /// <summary>
        /// 主要接待人员（中方）
        /// </summary>
        public List<VisitPerson> OurPersons { get; set; }

        /// <summary>
        /// 其他人员（中方）
        /// </summary>
        //public List<VisitPerson> OurOtherPersons { get; set; }
        public string OurOtherPersonStr { get; set; }

        /// <summary>
        /// 来访机构（外方）
        /// </summary>
        public List<VisitOrg> TheyOrgs { get; set; }

        /// <summary>
        /// 领队（外方）
        /// </summary>
        public List<VisitPerson> TheyPersons { get; set; }

        //public List<VisitPerson> TheyOtherPersons { get; set; }
        public string TheyOtherPersonStr { get; set; }


        public List<VisitOrg>  BeViOrgs { get; set; }

        /// <summary>
        /// 上级往来文件
        /// </summary>
        public List<VisitFile> SJWLFiles { get; set; }

        /// <summary>
        /// 来宾往来文件
        /// </summary>
        public List<VisitFile> LBWLFiles { get; set; }

        /// <summary>
        /// 内部管理文件
        /// </summary>
        public List<VisitFile> NBGLFiles { get; set; }


        /// <summary>
        /// 会议相关文件
        /// </summary>
        public List<VisitFile> HYXGFiles { get; set; }

        /// <summary>
        /// 新闻稿
        /// </summary>
        public List<VisitFile> NewsFiles { get; set; }

        /// <summary>
        /// 其他文件
        /// </summary>
        public List<VisitFile> OtherFiles { get; set; }
    }
}