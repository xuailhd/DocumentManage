﻿using DocumentManage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DocumentManage.Dtos
{
    public class RequestEditUserRoleDTO
    {
        public List<ResponseRoleDTO> RoleLists { get; set; }

        public string ID { get; set; }
    }
}