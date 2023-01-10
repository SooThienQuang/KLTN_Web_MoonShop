using KLTN_Web_MoonShop.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace KLTN_Web_MoonShop.Controllers
{
    public class SearchController : Controller
    {
        // GET: Search
        DBCosmeticEntities db = new DBCosmeticEntities();
        public ActionResult Search(string keyword)
        {
            
            ViewBag.ProductType = db.ProductTypes.Where(n => n.isActive == 1).OrderBy(n => n.proTypeName).ToList();
          
            List<Product> lstpro = new List<Product>();
            if (keyword != ""&&keyword.Length>0)
            {
                  try
                {
                    int type = int.Parse(keyword);
                  
                    var lstt = db.Products.Where(n => n.productTypeID == type && n.isActive == 1).ToList();
                    if (lstt.Count == 0&&type!=0)
                    {
                        return RedirectToAction("florder", new {sdt=keyword});
                    }
                    else
                    {
                        ViewBag.keyword = db.ProductTypes.FirstOrDefault(n => n.isActive == 1 && n.proTypeID == type).proTypeName;
                        StringBuilder hr = new StringBuilder();
                        ViewBag.type = type;
                        lstpro = lstt;
                    }
                }
                catch
                {
                    ViewBag.keyword = keyword;
                    ReplaceUnitcode replaceUnitcode = new ReplaceUnitcode();
                    string key1 = replaceUnitcode.RemoveUnicode(keyword).ToLower();
                    foreach(var item in db.Products.Where(n=>n.isActive==1).ToList())
                    {
                        string name1 = replaceUnitcode.RemoveUnicode(item.productName).ToLower();
                        if(name1.Contains(key1))
                        {
                            lstpro.Add(item);
                        }    
                    }    
                   // lstpro = db.Products.Where(n =>replaceUnitcode.RemoveUnicode(n.productName.ToLower()).Contains(key1) && n.isActive == 1).ToList();

                }
            }
            else
                lstpro = db.Products.Where(n => n.isActive == 1).ToList();
            return View(lstpro);
        }
        [HttpPost]
        public ActionResult Search(int txtmin,int txtmax,string cbbloaisp)
        {
            List<Product> lstpro = new List<Product>();
            int loai = int.Parse(cbbloaisp);
            lstpro = db.Products.Where(n => n.productPrice>=txtmin&&n.productPrice<=txtmax && n.isActive == 1&&n.productTypeID==loai).ToList();
            return View(lstpro);
        }
        public ActionResult florder(string sdt)
        {
            ViewBag.sdt = sdt;
            var lst = db.Orders.Where(n => n.phone.Equals(sdt)).ToList().OrderByDescending(n=>n.orderID).ToList();
            return View(lst);
        }
        public ActionResult revoke(string sdt,string id,string reason,string more)
        {
            long odid = long.Parse(id);
            Order d = db.Orders.FirstOrDefault(n => n.orderID == odid);
            if (d != null)
            {
                d.status = 5;
                d.reason = reason + ";" + more;
                db.Orders.AddOrUpdate(d);
                db.SaveChanges();
            }    
            return RedirectToAction("florder", new {sdt=sdt});
        }
        public ActionResult returnOrder(string txtreturn,string stdkk,string idkk, HttpPostedFileBase img1, HttpPostedFileBase img2, HttpPostedFileBase img3)
        {
            long id = long.Parse(idkk);
            if (img1!=null)
            {
                image imgg = db.images.ToList().LastOrDefault();
                string _FileName = Path.GetFileName(img1.FileName);
                string _path = Path.Combine(Server.MapPath("~/Asset/img/user"), _FileName);
                img1.SaveAs(_path);
                image hinh1 = new image();
                hinh1.id = imgg.id +1;
                hinh1.objectID = id;
                hinh1.name = img1.FileName;
                hinh1.isActive = 1;
                db.images.AddOrUpdate(hinh1);
                db.SaveChanges();
            }
            if (img2 != null)
            {
                image imgg = db.images.ToList().LastOrDefault();
                string _FileName = Path.GetFileName(img2.FileName);
                string _path = Path.Combine(Server.MapPath("~/Asset/img/user"), _FileName);
                img1.SaveAs(_path);
                image hinh1 = new image();
                hinh1.id = imgg.id+1;
                hinh1.name = img2.FileName;
                hinh1.isActive = 1;
                hinh1.objectID = id;
                db.images.AddOrUpdate(hinh1);
                db.SaveChanges();
            }
            if (img3 != null)
            {
                image imgg = db.images.ToList().LastOrDefault();
                string _FileName = Path.GetFileName(img3.FileName);
                string _path = Path.Combine(Server.MapPath("~/Asset/img/user"), _FileName);
                img1.SaveAs(_path);
                image hinh1 = new image();
                hinh1.id = imgg.id+1;
                hinh1.objectID = id;
                hinh1.name = img3.FileName;
                hinh1.isActive = 1;
                db.images.AddOrUpdate(hinh1);
                db.SaveChanges();
            }
            Order od = db.Orders.FirstOrDefault(n => n.orderID == id);
            if (od != null)
                od.status = 6;
            od.reason = txtreturn;
            db.Orders.AddOrUpdate(od);
            db.SaveChanges();
            if (stdkk==null)
                return RedirectToAction("index", "home");
            else
                return RedirectToAction("florder", "search", new { sdt = stdkk });
        }
    }
}