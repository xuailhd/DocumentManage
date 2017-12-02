using DocumentManage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DocumentManage.Dtos
{
    public class ResponseVisitListDTO
    {
        public string VisitID { get; set; }

        public string VisitType { get; set; }

        public string VisitName { get; set; }

        public string VisitFor { get; set; }

        public string MainDepartment { get; set; }

        public DateTime? FromDate { get; set; }
        public DateTime? EndDate { get; set; }

        public string VisType { get; set; }

        public string FeeType { get; set; }

        public string PayType { get; set; }

        public string TakeLevel { get; set; }

        public string IsLine { get; set; }

        public string AnsLevel { get; set; }

        public string Remark { get; set; }

        public string CreateUserID { get; set; }

        public string CreateUserName { get; set; }

        public DateTime CreateTime { get; set; }
    }

}