using KLTN_Web_MoonShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
            ViewBag.keyword=keyword;
            List<Product> lstpro = new List<Product>();
            if (keyword != "")
            {
                  try
                {
                    int type = int.Parse(keyword);
                    lstpro = db.Products.Where(n => n.productTypeID==type && n.isActive == 1).ToList();
                }
                catch
                {
                    lstpro = db.Products.Where(n => n.productName.Contains(keyword) && n.isActive == 1).ToList();

                }
            }
            //if (txtmin != 0 && txtmax != 0)
            //{
            //    lstpro = db.Products.Where(n => n.productPrice > txtmin && n.productPrice <= txtmax).ToList();
            //}
            else
                lstpro = db.Products.Where(n => n.isActive == 1).ToList();
            ViewBag.keyword = keyword;
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
    }
}