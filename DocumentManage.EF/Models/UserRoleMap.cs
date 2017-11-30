using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace DocumentManage.Models
{
    public class UserRoleMap
    {
        [Key]
        public string MapID { get; set; }
        public string UserID { get; set; }

        public string RoleID { get; set; }
    }
}