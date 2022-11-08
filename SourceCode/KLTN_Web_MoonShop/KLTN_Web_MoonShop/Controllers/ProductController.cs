using KLTN_Web_MoonShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KLTN_Web_MoonShop.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        DBCosmeticEntities db = new DBCosmeticEntities();
        public ActionResult Home()
        {   
            return PartialView(db.Products.Where(n=>n.isActive==1).ToList());
        }
        public ActionResult AddOrUpdate(int id)
        {
            return PartialView();
        }
        public ActionResult CaroselProductHome()
        {
            List<Product> lst = db.Products.Where(n=>n.isActive==1).Take(6).ToList();
            if (lst.Count > 0)
            {
                long id = lst.LastOrDefault().productID;
                ViewBag.Active = lst;
                ViewBag.Pro = db.Products.Where(n => n.productID >= id && n.isActive == 1).Take(6).ToList();
            }
            return PartialView();
        }
        public ActionResult Detail(long id)
        {
            ViewBag.Product = db.Products.FirstOrDefault(n => n.productID == id);
            ViewBag.ProductDetail = db.ProductDetails.FirstOrDefault(n => n.ProductID == id);
            return View();
        }
    }
}