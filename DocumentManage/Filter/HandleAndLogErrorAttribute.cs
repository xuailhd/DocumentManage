using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DocumentManage.Filters
{
    public class HandleAndLogErrorAttribute : HandleErrorAttribute
    {
        
        public override void OnException(ExceptionContext filterContext)
        {
            //KMEHosp.Common.LogHelper.WriteError(filterContext.Exception.GetDetailException());            
            base.OnException(filterContext);
        }
    }
}
