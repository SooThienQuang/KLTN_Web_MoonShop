using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KLTN_Web_MoonShop.Controllers
{
    public class PaymentController : Controller
    {
        // GET: Payment
        public ActionResult Checkout()
        {
            return View();
        }
        public ActionResult Detail()
        {
            return View();
        }
    }
}