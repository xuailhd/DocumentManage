using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace DocumentManage.Dtos
{
    public class ResponseAuthModelDTO
    {
        public string AuthID { get; set; }

        public string AuthName { get; set; }

        public bool Selected { get; set; }
    }
}