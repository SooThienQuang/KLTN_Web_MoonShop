﻿using KLTN_Web_MoonShop.Models;
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
            ViewBag.keyword=keyword;
            List<Product> lstpro = new List<Product>();
            if (keyword != "")
            {
                    lstpro = db.Products.Where(n => n.productName.Contains(keyword) && n.isActive == 1).ToList();
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
        public ActionResult Search(int txtmin,int txtmax)
        {
            List<Product> lstpro = new List<Product>();
            lstpro = db.Products.Where(n => n.productPrice>=txtmin&&n.productPrice<=txtmax && n.isActive == 1).ToList();
            return View(lstpro);
        }
    }
}