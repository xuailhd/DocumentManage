using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DocumentManage.Models
{
    public class BaseModel
    {
        public BaseModel()
        {
            CreateTime = DateTime.Now;
            IsDeleted = false;
        }
        public string CreateUserID { get; set; }

        public DateTime CreateTime { get; set; }

        public string ModifyUserID { get; set; }

        public DateTime? ModifyTime { get; set; }

        public bool IsDeleted { get; set; }

    }
}