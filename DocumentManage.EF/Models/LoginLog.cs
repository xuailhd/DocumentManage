using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace DocumentManage.Models
{
    public class LoginLog
    {
        [Key, Required]
        public string LogID { get; set; }

        public string LoginAccount { get; set; }

        public string LoginName { get; set; }

        public string LoginTime { get; set; }
    }
}