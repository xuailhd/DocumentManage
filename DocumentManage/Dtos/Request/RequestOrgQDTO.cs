using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DocumentManage.Dtos
{
    public class RequestOrgQDTO
    {
        public string OrgID { get; set; }

        public string OrgName { get; set; }

        public string Tag { get; set; }

        public string Level { get; set; }

        public string Address { get; set; }

        public string Continent { get; set; }

        public string Country { get; set; }

        public string UserID { get; set; }

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
        }
    }
}