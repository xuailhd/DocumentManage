using DocumentManage.EF;
using DocumentManage.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DocumentManage.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [Route("~/Login")]
        [IgnoreUserAuthenticate]
        public ActionResult Login()
        {
            using (var db = new DBEntities())
            {
                var user = db.Users.Where(t => t.UserID == "admin").FirstOrDefault();

            }
            return View();
        }
    }
}