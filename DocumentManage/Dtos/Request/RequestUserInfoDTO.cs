using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace DocumentManage.Dtos
{
    public class RequestUserInfoDTO
    {
        public string ID { get; set; }
        public string UserName { get; set; }

        public string UserID { get; set; }

        public string Password { get; set; }
    }
}