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
            return PartialView(db.Products.ToList());
        }
        public ActionResult AddOrUpdate(int id)
        {
            return PartialView();
        }
        public ActionResult CaroselProductHome()
        {
            List<Product> lst = db.Products.Take(6).ToList();
            long id = lst.LastOrDefault().productID;
            ViewBag.Active = lst;
            ViewBag.Pro = db.Products.Where(n => n.productID >= id).Take(6).ToList();
            return PartialView();
        }
        public ActionResult Detail(long id)
        {
            return View();
        }

    }
}