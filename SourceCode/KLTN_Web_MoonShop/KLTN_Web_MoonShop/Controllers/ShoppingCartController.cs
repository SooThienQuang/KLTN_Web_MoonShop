using KLTN_Web_MoonShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
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
               lst = db.CartDetails.Where(n => n.cartID == cart.cartID&&n.isActive==1).ToList();
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
                if(cart!=null)
                lst = db.CartDetails.Where(n => n.cartID == cart.cartID && n.isActive == 1).ToList();
                return PartialView(lst);
            }
            else
            {
                if (Request.Cookies["CartCookie"] != null)
                {
                    string chuoi = Request.Cookies["CartCookie"].Value.ToString();
                    string[] lst1 = chuoi.Split('|');
                    for (int i = 0; i < lst1.Length; i++)
                    {
                        try
                        {
                            string[] l = lst1[i].Split(',');
                            long proid1 = long.Parse(l[0]);
                            int quantity = int.Parse(l[1]);
                            CartDetail c = new CartDetail();
                            c.productID = proid1;
                            c.cartQuantity = quantity;
                            lst.Add(c);
                        }
                        catch
                        {

                        }
                    }
                }
            }    
            return PartialView(lst);
        }
        public ActionResult EmryCart()
        {
            return View();
        }

        public ActionResult AddToCart(long productId, int quantity)
        {
            string lstvcatnew = "";
            if (Request.Cookies["CartCookie"] == null)
                Response.Cookies["CartCookie"].Value = productId + "," + quantity+"|";
            else
            {
                string chuoi = Request.Cookies["CartCookie"].Value.ToString();
                string[] lst = chuoi.Split('|');
                for(int i=0;i<lst.Length;i++)
                {
                    try
                    {
                        string[] l = lst[i].Split(',');
                        long proid = long.Parse(l[0]);
                        if (proid == productId)
                        {
                            int sl = int.Parse(l[1])+quantity;
                            lstvcatnew =lstvcatnew+ l[0] + "," + sl + "|";
                        }
                        else
                        {
                            lstvcatnew =lstvcatnew+ l[0] + "," + l[1]+"|";
                        }    
                    }
                    catch
                    {

                    }
                }      
               
            }
            if(lstvcatnew == Request.Cookies["CartCookie"].Value)
            {
                lstvcatnew =lstvcatnew+ productId + "," + quantity + "|";
            }    
            Response.Cookies["CartCookie"].Value =lstvcatnew;
           Response.Cookies["CartCookie"].Expires = DateTime.Now.AddYears(30);
            return RedirectToAction("detail", "product", new {id=productId});
        }

        public ActionResult removeCartCookie(long id,int sl)
        {
            if (Request.Cookies["CartCookie"] != null)
            {

                string chuoi = Request.Cookies["CartCookie"].Value.ToString();
                chuoi = chuoi.Replace(id.ToString()+","+sl+"|", "");
                Response.Cookies["CartCookie"].Value=chuoi;
            }
            return RedirectToAction("detail");
        }
    }
}