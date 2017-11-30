using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DocumentManage.Dtos
{
    public class RequestChangePasswordDTO
    {
        public string UserID { get; set; }

        public string Password { get; set; }

        public string OldPassword { get; set; }
    }
}