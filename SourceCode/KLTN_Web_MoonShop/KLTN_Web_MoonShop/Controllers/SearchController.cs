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
            
            ViewBag.ProductType = db.ProductTypes.Where(n => n.isActive == 1).OrderBy(n => n.proTypeName).ToList();
          
            List<Product> lstpro = new List<Product>();
            if (keyword != ""&&keyword.Length>0)
            {
                  try
                {
                    int type = int.Parse(keyword);

                    ViewBag.keyword = db.ProductTypes.FirstOrDefault(n => n.isActive == 1 && n.proTypeID == type).proTypeName;
                    lstpro = db.Products.Where(n => n.productTypeID==type && n.isActive == 1).ToList();
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
    }
}