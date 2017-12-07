using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace DocumentManage.Models
{
    public class ResponseRoleDTO
    {
        [Key]
        public string RoleID { get; set; }

        public string RoleName { get; set; }

        public bool Selected { get; set; }

        public bool IsSystem { get; set; }

    }
}