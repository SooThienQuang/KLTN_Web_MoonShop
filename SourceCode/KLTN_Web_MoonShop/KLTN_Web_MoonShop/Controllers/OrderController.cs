using KLTN_Web_MoonShop.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
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
            ViewBag.id= id;
            ProcessOrder pd=db.ProcessOrders.FirstOrDefault(n=>n.objectID==id);
            if(pd!=null)
            {
                ViewBag.process = pd;
                Order d = db.Orders.FirstOrDefault(n => n.orderID == id);
                ViewBag.statusOd = d.status;
                ViewBag.shipper = db.EmployeeDetails.FirstOrDefault(n => n.emID == pd.receiveUserID);
            }    
            List<OrderDetail> lst = db.OrderDetails.Where(n => n.orderID == id).ToList();   
            return PartialView(lst);
        }
        public ActionResult Details(long id,string sdt)
        {
            ViewBag.id = id;
            ProcessOrder pd = db.ProcessOrders.FirstOrDefault(n => n.objectID == id);
            if (pd != null)
            {
                ViewBag.process = pd;
                Order d = db.Orders.FirstOrDefault(n => n.orderID == id);
                if(d.status==2)
                {
                    ViewBag.statusOd = "Đang chờ lấy hàng";
                }
                if (d.status == 3)
                {
                    ViewBag.statusOd = "Đang giao hàng";
                }
                if (d.status == 4)
                {
                    ViewBag.statusOd = "Giao thành công";
                }
                ViewBag.shipper = db.EmployeeDetails.FirstOrDefault(n => n.emID == pd.receiveUserID);
            }
            else
            {
                ViewBag.statusOd = "Chờ xác nhận";
            }    
            List<OrderDetail> lst = db.OrderDetails.Where(n => n.orderID == id).ToList();
            if (sdt != null)
                ViewBag.sdt = sdt;
            return View(lst);
        }
        public ActionResult List(long id)
        {
            Customer user = Session["user"] as Customer;
            List<Order> lst = new List<Order>();
            if (user != null)
            {
                if(id==5)
                    lst = db.Orders.Where(n => n.customerID == user.customerID && n.status == id||n.status==6).OrderByDescending(n => n.orderID).ToList();
                else
                    lst = db.Orders.Where(n => n.customerID == user.customerID&&n.status==id).OrderByDescending(n=>n.orderID).ToList();

            }
            return PartialView(lst);
        }
        public ActionResult fl(long id)
        {
            ViewBag.id = id;
            ProcessOrder pd = db.ProcessOrders.FirstOrDefault(n => n.objectID == id);
            if (pd != null)
            {
                ViewBag.process = pd;
                Order d = db.Orders.FirstOrDefault(n => n.orderID == id);
                if (d.status == 2)
                {
                    ViewBag.statusOd = "Đang chờ lấy hàng";
                }
                if (d.status == 3)
                {
                    ViewBag.statusOd = "Đang giao hàng";
                }
                if (d.status == 4)
                {
                    ViewBag.statusOd = "Giao thành công";
                }
                ViewBag.shipper = db.EmployeeDetails.FirstOrDefault(n => n.emID == pd.receiveUserID);
            }
            else
            {
                ViewBag.statusOd = "Chờ xác nhận";
            }
            List<OrderDetail> lst = db.OrderDetails.Where(n => n.orderID == id).ToList();
            return View(lst);
        }
        public ActionResult revoke(long id,string reason,string more)
        {
            try
            {
                Order od = db.Orders.FirstOrDefault(n => n.orderID == id);
                od.status = 5;
                od.reason = reason + ";" + more;
                db.Orders.AddOrUpdate(od);
                db.SaveChanges();
                return RedirectToAction("DetailProfile", "Customer",new {id=2});
            }
            catch
            {
                return RedirectToAction("DetailProfile", "Customer", new {id=2});

            }
        }
    }
}