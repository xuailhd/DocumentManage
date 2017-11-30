using DocumentManage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DocumentManage.Dtos
{
    public class ResponseVisitRecordDTO
    {
        public string VisitID { get; set; }

        public string VisitType { get; set; }

        public string VisitName { get; set; }

        public string VisitFor { get; set; }

        public string MianDepartment { get; set; }

        public DateTime FromDate { get; set; }
        public DateTime EndDate { get; set; }

        public string VisitTag { get; set; }

        public string VisType { get; set; }

        public string FeeType { get; set; }

        public string PayType { get; set; }

        public string TakeLevel { get; set; }

        public string IsLine { get; set; }

        public string AnsLevel { get; set; }

        public string Remark { get; set; }

        public string CreateUserID { get; set; }

        public List<VisitDetail> VisitDetails { get; set; }

        /// <summary>
        /// 主要经办人
        /// </summary>
        public List<ResponseVisitPerson> MianPersons { get; set; }

        /// <summary>
        /// 接待机构（中方）
        /// </summary>
        public List<ResponseVisitOrg> OurOrgs { get; set; }

        /// <summary>
        /// 主要接待人员（中方）
        /// </summary>
        public List<ResponseVisitPerson> OurPersons { get; set; }

        /// <summary>
        /// 其他人员（中方）
        /// </summary>
        public List<ResponseVisitPerson> OurOtherPersons { get; set; }

        /// <summary>
        /// 来访机构（外方）
        /// </summary>
        public List<ResponseVisitOrg> TheyOrgs { get; set; }

        /// <summary>
        /// 领队（外方）
        /// </summary>
        public List<ResponseVisitPerson> TheyMainPersons { get; set; }

        public List<ResponseVisitPerson> TheyOtherPersons { get; set; }

        public List<ResponseVisitOrg>  BeViOrgs { get; set; }

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

    public class ResponseVisitOrg
    {
        public string VisitOrgID { get; set; }

        public string OrgID { get; set; }
        public string VisitID { get; set; }

        /// <summary>
        /// 1-己方 2-外放 3-应邀
        /// </summary>
        public EnumOrgOwenType OwenType { get; set; }
        public string OrgName { get; set; }
        public string OrgNameEN { get; set; }

        public string ShortNameCN { get; set; }
        public string ShortNameEN { get; set; }
    }

    public class ResponseVisitPerson
    {
        public string VisitPersonID { get; set; }

        public string PersonID { get; set; }
        public string VisitID { get; set; }

        /// <summary>
        /// 0-主要经手人  1-己方 2-外放 3-应邀
        /// </summary>
        public EnumPersonOwenType OwenType { get; set; }

        /// <summary>
        /// 0-主要人员  1-其他人员
        /// </summary>
        public int Level { get; set; }

        public string NameCN { get; set; }

        public string NameEN { get; set; }
    }
}