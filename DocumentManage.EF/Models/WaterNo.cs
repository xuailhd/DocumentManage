using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace DocumentManage.Models
{
    public class WaterNo
    {
        [Key, Required]
        public string WaterNoID { get; set; }

        public int Type { get; set; }

        public int No { get; set; }
    }
}