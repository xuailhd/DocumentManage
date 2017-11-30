using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DocumentManage.Dtos
{
    public class RequestAuthDTO
    {
        public string UserID { get; set; }

        /// <summary>
        /// 0 菜单 1 功能权限
        /// </summary>
        public int Type { get; set; }
    }
}