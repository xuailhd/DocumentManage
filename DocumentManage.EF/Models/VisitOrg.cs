using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace DocumentManage.Models
{
    public class VisitOrg
    {
        [Key]
        public string VisitOrgID { get; set; }

        public string OrgID { get; set; }
        public string VisitID { get; set; }

        /// <summary>
        /// 1-己方 2-外放 3-应邀
        /// </summary>
        public EnumOrgOwenType OwenType { get; set; }
    }

    public enum EnumOrgOwenType
    {
        Our = 1,
        They = 2,
        BeVi = 3
    }
}