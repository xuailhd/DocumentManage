using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace DocumentManage.Models
{
    public class VisitDetail
    {
        [Key]
        public string DetailID { get; set; }

        public string VisitID { get; set; }

        public string No { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? EndDate { get; set; }

        public string Adress { get; set; }
        
    }
}