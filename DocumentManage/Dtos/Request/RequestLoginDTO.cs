using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DocumentManage.Dtos
{
    public class RequestLoginDTO
    {
        public string UserID { get; set; }

        public string Password { get; set; }
    }
}