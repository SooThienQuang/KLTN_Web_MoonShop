using KLTN_Web_MoonShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KLTN_Web_MoonShop.Controllers
{
    public class ShoppingCartController : Controller
    {
        DBCosmeticEntities db = new DBCosmeticEntities();
        // GET: ShoppingCart
        public ActionResult AddOrUpdate()
        {
            ViewBag.CreateSuccess = "Thêm thành công";
            return View();
        }
        public ActionResult AllCart()
        {
            Customer user = Session["user"] as Customer;
            List<CartDetail> lst = new List<CartDetail>();
            if (user!=null)
            {
              Cart cart=  db.Carts.FirstOrDefault(n => n.customerID == user.customerID);
               lst = db.CartDetails.Where(n => n.cartID == cart.cartID).ToList();
               return View(lst);
            }    
            return View(lst);
        }
        public ActionResult Detail()
        {
            Customer user = Session["user"] as Customer;
            List<CartDetail> lst = new List<CartDetail>();
            if (user != null)
            {
                Cart cart = db.Carts.FirstOrDefault(n => n.customerID == user.customerID);
                lst = db.CartDetails.Where(n => n.cartID == cart.cartID).ToList();
                return PartialView(lst);
            }
            return PartialView(lst);
        }
        public ActionResult EmryCart()
        {
            return View();
        }
    }
}