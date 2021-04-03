using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace DocumentManage.Models
{
    public class VisitFile
    {
        [Key]
        public string FileID { get; set; }

        public string FileName { get; set; }

        public string FileUrl { get; set; }

        /// <summary>
        /// 1-人员护照 2-人员头像 3-人员身份证复印件 
        /// 11-上级往来文件 12-来宾往来文件 13 -内部管理文件 14-会议相关文件 15-新闻稿 16-其他文件
        /// 20-背景
        /// </summary>
        public string Type { get; set; }

        public string OutID { get; set; }
    }
}