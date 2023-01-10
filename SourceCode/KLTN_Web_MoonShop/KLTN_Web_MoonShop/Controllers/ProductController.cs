using KLTN_Web_MoonShop.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace KLTN_Web_MoonShop.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        DBCosmeticEntities db = new DBCosmeticEntities();
        public ActionResult Home(int page = 1, int size = 30)
        {
            List<Product> all = db.Products.Where(n => n.isActive == 1).ToList();
            Customer customer = Session["user"] as Customer;
            if(customer!=null)
            {
                var ids = db.ActionLogs.Where(n=>n.userID==customer.customerID).Select(n => n.idOject).Distinct().ToList();
                var q = (from h in db.ActionLogs where h.userID==customer.customerID
                         group h by new { h.idOject } into hh
                         select new
                         {
                             hh.Key.idOject,
                             Score = hh.Sum(s => s.idOject)
                         }).OrderByDescending(i => i.Score).Distinct().Select(n=>n.idOject).ToList();
                if (ids.Any())
                {
                    List<Product> products = all.Where(n => q.Contains(n.productID)).ToList();
                    all.AddRange(products);
                }
               
            }
            return PartialView(all.Distinct().ToPagedList(page, size));
        }
        //------------------------------------bình luận---------------------------------------
        public ActionResult Comment(int page = 1, int size = 3)
        {
            var item = db.Products.Where(n => n.isActive == 1).ToList().ToPagedList(page, size);
            return PartialView(item);
        }
        public ActionResult AddOrUpdate(int id)
        {
            return PartialView();
        }
        public ActionResult CaroselProductHome()
        {
            List<Product> lst = db.Products.Where(n=>n.isActive==1).ToList().Take(6).ToList();
            if (lst.Count > 0)
            {
                long id = lst.LastOrDefault().productID;
                ViewBag.Active = lst;
                ViewBag.Pro = db.Products.Where(n => n.productID >= id && n.isActive == 1).ToList().Take(6).ToList();
            }
            return PartialView();
        }
        public ActionResult Detail(long id,string cumb)
        {
            if(cumb!=null)
            {
                try
                {
                    long c = long.Parse(cumb);
                    ProductType type = db.ProductTypes.FirstOrDefault(n => n.proTypeID == c);
                    ViewBag.cumname = type.proTypeName;
                    ViewBag.cumid = type.proTypeID;
                }
                catch { }
            }    
            Product pro = db.Products.FirstOrDefault(n => n.productID == id);
            ViewBag.Product = db.Products.FirstOrDefault(n => n.productID == id);
            ViewBag.ProductDetail = db.ProductDetails.FirstOrDefault(n => n.ProductID == id);
            ViewBag.ProductRecom = db.Products.Where(n => n.productTypeID == pro.productTypeID&&n.isActive==1).ToList();
            Customer user = Session["user"] as Customer;
            if(user!=null)
            {
                ActionLog acl = new ActionLog();
                string acid = DateTime.Now.ToString("yyyyMMddHHmmss");
                acl.actionLogID = long.Parse(acid);
                acl.actionDate = DateTime.Now;
                acl.userID = user.customerID;
                acl.userName = user.customerUserName;
                acl.idOject = id;
                acl.actionName = "Xem";
                acl.nameTable = "Product";
                acl.actionType = 1;
                acl.modun = "product/detail/"+id;
                db.ActionLogs.AddOrUpdate(acl);
                db.SaveChanges();
            }    
            return View();
        }
        public ActionResult ShowDetail(long id)
        {
            if(id>0)
            {
                ViewBag.Product = db.Products.FirstOrDefault(n => n.productID == id);
                ViewBag.ProductDetail = db.ProductDetails.FirstOrDefault(n => n.ProductID == id);
            }    
         
            return View();
        }
    }
}