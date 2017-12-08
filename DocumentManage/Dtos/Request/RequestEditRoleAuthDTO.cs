using DocumentManage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DocumentManage.Dtos
{
    public class RequestEditRoleAuthDTO
    {
        public List<ResponseAuthModelDTO> AuthLists { get; set; }

        public int Type { get; set; }
        public string RoleID { get; set; }
    }
}