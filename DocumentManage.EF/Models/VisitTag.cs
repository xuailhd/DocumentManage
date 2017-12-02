using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace DocumentManage.Models
{
    public class VisitTag
    {
        [Key]
        public string VisitTagID { get; set; }

        public string VisitID { get; set; }

        public string Name { get; set; }
        
    }
}