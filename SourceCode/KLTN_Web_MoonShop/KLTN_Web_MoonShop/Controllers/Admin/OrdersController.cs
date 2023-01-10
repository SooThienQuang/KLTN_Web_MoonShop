using KLTN_Web_MoonShop.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using PagedList;
namespace KLTN_Web_MoonShop.Controllers.Admin
{
    public class OrdersController : Controller
    {
        // GET: Orders
        DBCosmeticEntities db = new DBCosmeticEntities();
        public ActionResult Waiting(int id , int page = 1, int size = 5)
        {
            ViewBag.id = id;
            ViewBag.stt = db.Status.FirstOrDefault(n=>n.statusID==id);
            var lstorder = db.Orders.Where(n=>n.status==id).ToList().OrderByDescending(n => n.createDate).ToList().ToPagedList(page, size);
            return View(lstorder);
        }
        public ActionResult Showmore(long id)
        {
            ViewBag.id = id;
            Order d = db.Orders.FirstOrDefault(n => n.orderID == id);
            ViewBag.cus=db.Customers.FirstOrDefault(n=>n.customerID==d.customerID);
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
        public ActionResult detail(long id,string cumb,string notiID)
        {
            if(cumb.Count()>0)
            {
                long i = long.Parse(cumb);
                ViewBag.Name = db.Status.FirstOrDefault(n => n.statusID == i);
            }
            else
            {
                long idno = long.Parse(notiID);
                Notification noti = db.Notifications.FirstOrDefault(n => n.notiID == idno);
                noti.isRead = 1;
                db.Notifications.AddOrUpdate(noti);
                db.SaveChanges();
                Order d = db.Orders.FirstOrDefault(n => n.orderID == id);
                ViewBag.Name = db.Status.FirstOrDefault(n => n.statusID == d.status);
            }    
            List<OrderDetail> lst = db.OrderDetails.Where(n => n.orderID == id).ToList();
            return View(lst);
        }
    }
}