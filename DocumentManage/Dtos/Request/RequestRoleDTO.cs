using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace DocumentManage.Dtos
{
    public class RequestRoleDTO
    {
        public string ID { get; set; }
        public string RoleID { get; set; }

        public string RoleName { get; set; }

        public bool IsNew { get; set; }

    }
}