using KLTN_Web_MoonShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KLTN_Web_MoonShop.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        DBCosmeticEntities db = new DBCosmeticEntities();
        public ActionResult Tesst()
        {
            return View(db.Products.ToList());
        }
        public ActionResult Tesst1()
        {
            return View();
        }
        public ActionResult Tesst1cart()
        {
            return View();
        }
    }
}