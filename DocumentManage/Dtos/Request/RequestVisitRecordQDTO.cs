using DocumentManage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DocumentManage.Dtos
{
    public class RequestVisitRecordQDTO
    {
        public string UserID { get; set; }
        public string VisitID { get; set; }

        public string VisitType { get; set; }

        public string VisitName { get; set; }

        public string VisitFor { get; set; }

        public string MainDepartment { get; set; }

        public string MianPerson { get; set; }

        public DateTime? FromDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string VisitTag { get; set; }

        public string OurPerson { get; set; }

        public string OurOrg { get; set; }


        public string TheyPerson { get; set; }
        public string TheyOrg { get; set; }

        public string BeViOrg { get; set; }

        public string VisType { get; set; }

        public string FeeType { get; set; }

        private int _PageSize;
        public int PageSize
        {
            get
            {
                if (_PageSize <= 0)
                {
                    return 10;
                }
                return _PageSize;
            }
            set
            {
                _PageSize = value;
            }
        }

        private int _PageIndex;
        public int PageIndex
        {
            get
            {
                if (_PageIndex <= 0)
                {
                    return 1;
                }
                return _PageIndex;
            }
            set
            {
                _PageIndex = value;
            }
        }
    }
}