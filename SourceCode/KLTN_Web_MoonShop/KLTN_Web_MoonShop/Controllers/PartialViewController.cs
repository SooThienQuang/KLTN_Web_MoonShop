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
        public ActionResult Countdown()
        {
            return PartialView();
        }
        public ActionResult NavbarProducer()
        {
            return PartialView();
        }
        public ActionResult NavbarProductTop()
        {
            return PartialView();
        }

        public ActionResult CaroselHome()
        {
            image m = db.images.FirstOrDefault(n => n.type == 0&&n.isActive==1);   
            var item = db.images.Where(n => n.type == 0&&n.id!=m.id&&n.isActive==1).ToList();
            ViewBag.active = m;
            return PartialView(item);
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



        //public ActionResult Profile()
        //{

        //        Customer cs = Session["user"] as Customer;
        //        if(cs != null)
        //        {
        //            string ten = cs.customerName.ToString().Split(' ').Last();
        //            ViewBag.name = ten;
        //            ViewBag.user = cs;
        //        }    
               
        //        return PartialView();

        //}
        public ActionResult Suggest()
        {
         return PartialView();
        }
    }
}