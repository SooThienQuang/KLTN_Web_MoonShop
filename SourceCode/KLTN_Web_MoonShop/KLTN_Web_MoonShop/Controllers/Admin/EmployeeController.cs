using KLTN_Web_MoonShop.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KLTN_Web_MoonShop.Controllers.Admin
{
    public class EmployeeController : Controller
    {
        DBCosmeticEntities db = new DBCosmeticEntities();
        // GET: Employee
        public ActionResult Index()
        {
          return View(db.Products.Where(n => n.isActive == 1).OrderByDescending(n => n.productID).ToList());
        }
        [HttpPost]
        public ActionResult img(HttpPostedFileBase image)
        {
            if (image != null)
            {
                string _FileName = Path.GetFileName(image.FileName);
                string _path = Path.Combine(Server.MapPath("~/Asset/img/user"), _FileName);
                image.SaveAs(_path);
            }
            Customer cs = Session["user"] as Customer;

            if (cs != null)
            {
                cs.customerPhoto = image.FileName;
                db.Customers.AddOrUpdate(cs);
                db.SaveChanges();
                Session["user"] = cs;
            }
            return RedirectToAction("DetailProfile");
        }

    }
}