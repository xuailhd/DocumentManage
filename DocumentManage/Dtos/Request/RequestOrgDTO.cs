using DocumentManage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DocumentManage.Dtos.Request
{
    public class RequestOrgDTO: Orgnazition
    {
        /// <summary>
        /// 背景
        /// </summary>
        public List<VisitFile> BJFiles { get; set; }

        /// <summary>
        /// 其他文件
        /// </summary>
        public List<VisitFile> OtherFiles { get; set; }
    }
}