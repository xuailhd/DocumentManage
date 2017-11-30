using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace DocumentManage.Models
{
    public class AuthModel
    {
        [Key, Required]
        public string AuthID { get; set; }

        public string AuthName { get; set; }

        public string ParentID { get; set; }

        public string AuthUrl { get; set; }

        public string CSSClass { get; set; }

        public string Target { get; set; }

        public int OrderNo { get; set; }

        /// <summary>
        /// 0 菜单 1 功能权限
        /// </summary>
        public int Type { get; set; }
    }
}