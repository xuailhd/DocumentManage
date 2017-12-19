using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DocumentManage.Dtos
{
    public class RequestChangePasswordDTO
    {
        public string ID { get; set; }

        public string NewPassword { get; set; }

        public string OldPassword { get; set; }
    }
}