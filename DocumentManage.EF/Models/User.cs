using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace DocumentManage.Models
{
    public class User: BaseModel
    {
        public User()
        {
            State = 0;
        }

        [Key]
        public string ID { get; set; }
        public string UserID { get; set; }


        public string Password { get; set; }

        public string UserName { get; set; }

        public string UserToken { get; set; }

        public DateTime? LastTime { get; set; }

        public bool IsSystem { get; set; }

        /// <summary>
        /// 0-正常 1-冻结
        /// </summary>
        public int State { get; set; }
    }
}