using MoonShop.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MoonShop.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        DBCosmeticEntities db = new DBCosmeticEntities();
        public ActionResult Index()
        {
            return View(db.Tbl_Product.ToList());
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Tbl_Product b)
        {
            db.Tbl_Product.Add(b);
            db.SaveChanges();
            return View();
        }
    }
}