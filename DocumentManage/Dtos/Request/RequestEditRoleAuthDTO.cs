using DocumentManage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DocumentManage.Dtos
{
    public class RequestEditRoleAuthDTO
    {
        public List<string> AuthLists { get; set; }

        public string RoleID { get; set; }
    }
}