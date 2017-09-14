using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace APIQuery.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            int userid = GetLoggedCookie();
            if (userid > 0)
                return RedirectToAction("home", "register");
            else
                return RedirectToAction("login", "register");
        }
        public int GetLoggedCookie()
        {
            if (Request.Cookies["RLog"] != null)
            {
                var userid = Request.Cookies["RLog"].Value;
                HttpContext.Session["UserId"] = userid;
                return Convert.ToInt32(userid);
            }
            return 0;
        }
    }
}
