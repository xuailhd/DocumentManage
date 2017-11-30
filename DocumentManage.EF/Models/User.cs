using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace DocumentManage.Models
{
    public class User: BaseModel
    {
        [Key]
        public string UserID { get; set; }
        public string Password { get; set; }

        public string UserName { get; set; }

        public string UserToken { get; set; }

        public DateTime? LastTime { get; set; }
    }
}