using KLTN_Web_MoonShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KLTN_Web_MoonShop.Controllers
{
    public class PartialViewController : Controller
    {
        DBCosmeticEntities db = new DBCosmeticEntities();
        // GET: PartialView
        public ActionResult NavbarTop()
        {
            return PartialView();
        }
        public ActionResult NavbarProductTop()
        {
            return PartialView();
        }

        public ActionResult CaroselHome()
        {
            return PartialView();
        }
        

        public ActionResult CaroselProductHome()
        {
            List<Product>lst= db.Products.Take(6).ToList();
            long id = lst.LastOrDefault().productID;
            ViewBag.Active = lst;
            ViewBag.Pro = db.Products.Where(n => n.productID >= id).Take(6).ToList();
            return PartialView();
        }

        public ActionResult Whatsapp()
        {
            return PartialView();
        }
    }
}