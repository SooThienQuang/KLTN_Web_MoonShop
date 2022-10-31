using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KLTN_Web_MoonShop.Controllers
{
    public class ShoppingCartController : Controller
    {
        // GET: ShoppingCart
        public ActionResult AddOrUpdate()
        {
            ViewBag.CreateSuccess = "Thêm thành công";
            return View();
        }
        public ActionResult AllCart()
        {
            return View();
        }
        public ActionResult EmryCart()
        {
            return View();
        }
    }
}