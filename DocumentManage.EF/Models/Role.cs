using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace DocumentManage.Models
{
    public class Role
    {
        public Role()
        {
            State = 0;
        }

        [Key]
        public string ID { get; set; }
        public string RoleID { get; set; }

        public string RoleName { get; set; }

        public bool IsSystem { get; set; }

        /// <summary>
        /// 0-正常 2-冻结
        /// </summary>
        public int State { get; set; }

    }
}