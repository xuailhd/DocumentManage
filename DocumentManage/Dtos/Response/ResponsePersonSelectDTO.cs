using DocumentManage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DocumentManage.Dtos.Request
{
    public class ResponsePersonSelectDTO
    {
        public string PersonID { get; set; }
        public string OrgName { get; set; }
        public string NameCN { get; set; }
        public string NameEN { get; set; }

        public bool Selected { get; set; }
    }
}