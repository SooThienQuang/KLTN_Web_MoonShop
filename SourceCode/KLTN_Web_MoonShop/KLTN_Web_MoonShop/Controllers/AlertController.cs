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
        public ActionResult Success()
        {
            return PartialView();
        }
    }
}