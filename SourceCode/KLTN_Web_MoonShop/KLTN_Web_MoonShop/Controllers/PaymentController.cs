using KLTN_Web_MoonShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;

namespace KLTN_Web_MoonShop.Controllers
{
    public class request
    {
        public string chuoi { get; set; }
    }

    public class PaymentController : Controller
    {
        DBCosmeticEntities db = new DBCosmeticEntities();
        // GET: Payment
        public ActionResult Checkout()
        {
            return View();
        }
        public ActionResult Detail()
        {
            return View();
        }
        public ActionResult Paymore(string id)
        {
            Customer user = Session["user"]as Customer;
            //id
            ViewBag.id = id;
            //gio hang
            Cart cart = db.Carts.FirstOrDefault(n => n.customerID == user.customerID);
            //
            ViewBag.cartID = cart.cartID;
            //danh sach tat ca chi tiet gio hang
            List<CartDetail> lstcartdeall = db.CartDetails.Where(n => n.cartID == cart.cartID && n.isActive == 1).ToList();
            //chi tiet san pham da chon tu gio hang
            List<CartDetail> lstd = new List<CartDetail>();
            List<Product> lst = new List<Product>();
            List<long> proid = new List<long>();
            if(id.Equals("getallcartqnt"))
            {
                if(user!=null)
                {
                    foreach(CartDetail detail in lstcartdeall)
                    {
                        Product d = db.Products.FirstOrDefault(n => n.productID == detail.productID);
                        lst.Add(d);
                    }    
                }
                lstd = lstcartdeall;
                ViewBag.CartDetail = lstd;
                ViewBag.allMoney = lstd.Sum(n => n.cartMoney);
            }
            else
            {
                string[] a = id.Split(',');
                var unique_items = new HashSet<string>(a);
                foreach (string s in unique_items)
                {
                    if (s != null && s.Equals("") == false)
                    {
                        proid.Add(long.Parse(s));
                    }
                }

                foreach (long pro in proid)
                {
                    Product pro1 = db.Products.FirstOrDefault(n => n.productID == pro);
                    lst.Add(pro1);
                   
                    foreach(var item in lstcartdeall)
                    {
                        if(item.productID==pro)
                        {
                            lstd.Add(item);
                        }    
                    }    
                }
                ViewBag.CartDetail = lstd;
                ViewBag.allMoney = lstd.Sum(n => n.cartMoney);
            }    
            
            return PartialView(lst);
        }

    }
}