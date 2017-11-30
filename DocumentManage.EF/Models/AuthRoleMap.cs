using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace DocumentManage.Models
{
    public class AuthRoleMap
    {
        [Key, Required]
        public string MapID { get; set; }
        
        public string AuthID { get; set; }

        public string RoleID { get; set; }
    }
}