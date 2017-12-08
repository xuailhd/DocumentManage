using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace DocumentManage.Dtos
{
    public class ResponseOrgnazitionDTO
    {
        public string OrgID { get; set; }

        public string OrgName { get; set; }

        public string FromType { get; set; }

        public string ShortNameCN { get; set; }

        public string OrgNameEN { get; set; }
        public string ShortNameEN { get; set; }

        public string Tag { get; set; }

        public string Level { get; set; }

        public string Address { get; set; }

        /// <summary>
        /// 洲
        /// </summary>
        public string Continent { get; set; }

        public string Country { get; set; }

        public string Province { get; set; }

        public string OrgType { get; set; }

        public string OrgBack { get; set; }

        public string OrgInfo { get; set; }

        public string WorkAddress { get; set; }

        public string WorkTime { get; set; }

        public string ContactPerson1 { get; set; }

        public string ContactPerson2 { get; set; }

        public string Tel1 { get; set; }

        public string Tel2 { get; set; }

        public string Email1 { get; set; }

        public string Email2 { get; set; }

        public string Remark { get; set; }

        public string CreateUserID { get; set; }

        public string CreateUserName { get; set; }

        public DateTime CreateTime { get; set; }

        public string ModifyUserName { get; set; }

        public DateTime? ModifyTime { get; set; }
    }
}