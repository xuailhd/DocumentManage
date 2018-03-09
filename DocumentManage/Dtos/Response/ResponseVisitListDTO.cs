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

        public string MianPersonStr { get; set; }
        public string TheyPersonStr { get; set; }

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

        public string ModifyUserName { get; set; }

        public DateTime? ModifyTime { get; set; }

        public List<string> MainPersons { get; set; }

        public List<string> OurPersons { get; set; }

        //public List<string> OurOPersons { get; set; }
        public string OurOtherPersonStr { get; set; }

        public List<string> TheyPersons { get; set; }

        //public List<string> TheyOPersons { get; set; }
        public string TheyOtherPersonStr { get; set; }

        public List<string> OurOrgs { get; set; }

        public List<string> TheyOrgs { get; set; }

        public List<string> VisitTags { get; set; }

        public List<string> BeviOrgs { get; set; }
    }

}