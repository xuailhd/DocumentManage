using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DocumentManage.Dtos
{
    public class RequestRoleQDTO
    {
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