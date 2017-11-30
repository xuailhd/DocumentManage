using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace DocumentManage.Models
{
    public class VisitPerson
    {
        [Key]
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
        public int  Level { get; set; }
    }
    public enum EnumPersonOwenType
    {
        MainHanle = 0,
        Our = 1,
        They = 2,
        BeVi = 3
    }
}