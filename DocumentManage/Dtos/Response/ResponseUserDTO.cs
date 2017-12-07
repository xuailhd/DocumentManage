using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using DocumentManage.Models;

namespace DocumentManage.Dtos
{
    public class ResponseUserDTO
    {
        public string UserID { get; set; }

        public string UserName { get; set; }

        public DateTime? LastTime { get; set; }

        public string LastTimeStr { get; set; }

        public bool IsSystem { get; set; }

        public List<ResponseRoleDTO> Roles {get;set;}

        public string RolesStr { get; set; }
    }


}
