using KLTN_Web_MoonShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KLTN_Web_MoonShop.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        DBCosmeticEntities db = new DBCosmeticEntities();
        public ActionResult Detail(long id)
        {
            List<OrderDetail> lst = db.OrderDetails.Where(n => n.orderID == id).ToList();   
            return PartialView(lst);
        }
    }
}