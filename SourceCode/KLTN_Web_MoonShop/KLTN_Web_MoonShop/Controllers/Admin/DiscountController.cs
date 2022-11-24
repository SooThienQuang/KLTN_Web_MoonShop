using KLTN_Web_MoonShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KLTN_Web_MoonShop.Controllers.Admin
{
    public class DiscountController : Controller
    {
        // GET: Discount
        DBCosmeticEntities db = new DBCosmeticEntities();
        public ActionResult Index()
        {
            
            return View(db.Discounts.Where(n=>n.isActive==1).ToList());
        }
    }
}