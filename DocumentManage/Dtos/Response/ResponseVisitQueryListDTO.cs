using DocumentManage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DocumentManage.Dtos
{
    public class ResponseVisitQueryListDTO
    {
        public PagedList<ResponseVisitListDTO> VisitRecords { get; set; }

        public int CountryCount { get; set; }
        public int QGLHCount { get; set; }
        public int GZCount { get; set; }
        public int TZBCount { get; set; }
        public int ZFJGCount { get; set; }
        public int FWYXCount { get; set; }
        public int QTCount { get; set; }
    }

}