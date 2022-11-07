using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KLTN_Web_MoonShop.Controllers
{
    public class AlertController : Controller
    {
        // GET: Alert
        public ActionResult Success(string message)
        {
            ViewBag.message = message;
            return PartialView();
        }
        public ActionResult Fail(string message)
        {
            ViewBag.message = message;
            return PartialView();
        }

        public ActionResult NotLogin()
        {
            return PartialView();
        }
    }
}