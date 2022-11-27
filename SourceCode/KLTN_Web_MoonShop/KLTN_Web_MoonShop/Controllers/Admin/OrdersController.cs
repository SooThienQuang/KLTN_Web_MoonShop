using KLTN_Web_MoonShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KLTN_Web_MoonShop.Controllers.Admin
{
    public class OrdersController : Controller
    {
        // GET: Orders
        DBCosmeticEntities db = new DBCosmeticEntities();
        public ActionResult Waiting()
        {
            
            return View();
        }
        public ActionResult Showmore(long id)
        {
            ViewBag.id = id;
            Order d = db.Orders.FirstOrDefault(n => n.orderID == id);
            ViewBag.cus=db.Customers.FirstOrDefault(n=>n.isActive==1&&n.customerID==d.customerID);
            List<OrderDetail> lst = db.OrderDetails.Where(n => n.orderID == id).ToList();
            return PartialView(lst);
        }
            public ActionResult Processing()
        {
            return View(db.Orders.Where(n => n.status == 2).ToList());
        }
        public ActionResult Processed()
        {
            return View(db.Orders.Where(n => n.status == 3).ToList());
        }
    }
}