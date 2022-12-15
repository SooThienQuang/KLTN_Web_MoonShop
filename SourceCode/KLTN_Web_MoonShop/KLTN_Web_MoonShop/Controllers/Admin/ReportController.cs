using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KLTN_Web_MoonShop.Controllers.Admin
{
    public class ReportController : Controller
    {
        // GET: Report
        public ActionResult order()
        {
            return View();
        }
        public ActionResult product()
        {
            return View();
        }
        public ActionResult cart()
        {
            return View();
        }
        public ActionResult actionuser()
        {
            return View();
        }
        public ActionResult money()
        {
            return View();
        }
    }
}