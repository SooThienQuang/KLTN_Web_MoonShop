using KLTN_Web_MoonShop.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KLTN_Web_MoonShop.Controllers.Admin
{
    public class ProductTypeController : Controller
    {
        // GET: ProductType
        DBCosmeticEntities db = new DBCosmeticEntities();
        public ActionResult List()
        {
            return View(db.ProductTypes.Where(n=>n.isActive==1).ToList());
        }
        public ActionResult AddOrUpdate(int id)
        {
            if(id == 0)
            {
                ViewBag.Title = "Thêm mới loại sản phẩm";
                return PartialView();
            }
            else
            {
                ViewBag.Title = "Sửa loại sản phẩm";
                return PartialView(db.ProductTypes.FirstOrDefault(n => n.proTypeID == id));
            }    
          
        
        }
        [HttpPost]
        public ActionResult AddOrUpdate(int id,string txtprotypename)
        {
            if(id==0)
            {
                ProductType proid = db.ProductTypes.ToList().LastOrDefault();
                ProductType protype = new ProductType();
                protype.proTypeID = proid.proTypeID + 1;
                protype.proTypeName = txtprotypename;
                protype.isActive = 1;
                db.ProductTypes.AddOrUpdate(protype);
                db.SaveChanges();
                return RedirectToAction("List","ProductType");
            }
            else
            {
                ProductType protype = new ProductType();
                protype.proTypeID = id;
                protype.proTypeName = txtprotypename;
                protype.isActive = 1;
                db.ProductTypes.AddOrUpdate(protype);
                db.SaveChanges();
                return RedirectToAction("List", "ProductType");
            }    
        }

            public ActionResult Delete(int id)
        {
           try
            {
                ProductType p = db.ProductTypes.FirstOrDefault(n => n.proTypeID == id);
                p.isActive = 0;
                db.ProductTypes.AddOrUpdate(p);
                db.SaveChanges();
                return RedirectToAction("List","ProducType");
            }
            catch
            {
                return HttpNotFound();
            }
        }
    }
}