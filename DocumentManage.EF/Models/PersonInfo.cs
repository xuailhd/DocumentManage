using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace DocumentManage.Models
{
    public class PersonInfo : BaseModel
    {
        [Key]
        public string PersonID { get; set; }

        public string FromType { get; set; }

        public string OrgID { get; set; }
        public string OrgName { get; set; }
        public string NameCN { get; set; }
        public string NameEN { get; set; }
        public string Tag { get; set; }

        public string Department { get; set; }
        public string PassportCode { get; set; }
        public string PassportUrlName { get; set; }
        public string PassportUrl { get; set; }

        public DateTime? PassportDate { get; set; }

        public DateTime? PassportSignDate { get; set; }

        public string PassportSignAdress { get; set; }

        public string PassportType { get; set; }

        public string PhotoUrl { get; set; }

        public string PhotoUrlName { get; set; }

        public string Title { get; set; }

        public string IDNumber { get; set; }
        public string IDNumberUrl { get; set; }

        public string IDNumberUrlName { get; set; }

        public string Duty { get; set; }

        public string Email { get; set; }

        public string Tel1 { get; set; }

        public string Tel2 { get; set; }

        public string Mobile1 { get; set; }

        public string Mobile2 { get; set; }

        public string ContactAddress { get; set; }

        public string Sex { get; set; }

        public DateTime? Birth { get; set; }

        public string Nationality { get; set; }
        public string Fancy { get; set; }

        public string Taboo { get; set; }

        public string RecLevel { get; set; }

        public string Remark { get; set; }
    }
}