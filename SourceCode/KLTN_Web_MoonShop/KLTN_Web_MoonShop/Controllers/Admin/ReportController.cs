using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KLTN_Web_MoonShop.Models;
namespace KLTN_Web_MoonShop.Controllers.Admin
{
    public class ReportController : Controller
    {
        // GET: Report
        public ActionResult order()
        {
            return View();
        }
        public ActionResult product()
        {
            return View();
        }
        public ActionResult cart()
        {
            DBCosmeticEntities db = new DBCosmeticEntities();
            var q = (from h in db.CartDetails
                     group h by new { h.productID } into hh
                     select new
                     {
                         hh.Key.productID,
                         Score = hh.Sum(s => s.cartQuantity)
                     }).OrderByDescending(i => i.Score).ToList();
            List<cartTam> lst = new List<cartTam>();
            foreach (var a in q)
            {
                cartTam cd = new cartTam();
                cd.id = a.productID;
                Product pro = db.Products.FirstOrDefault(n => n.productID == a.productID);
                cd.name = pro.productName.Substring(0,30);
                cd.quantity = (long)a.Score;
                lst.Add(cd);
            }    
            if(lst!=null)
            {
                ViewBag.chart=lst.Take(10);
            }    
          
            return View();
        }
        public ActionResult actionuser()
        {
            return View();
        }
        public ActionResult money()
        {
            return View();
        }
    }
}