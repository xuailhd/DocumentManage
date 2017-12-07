using DocumentManage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DocumentManage.Dtos
{
    public class RequestEditUserRoleDTO
    {
        public List<string> RoleLists { get; set; }

        public string UserID { get; set; }
    }
}