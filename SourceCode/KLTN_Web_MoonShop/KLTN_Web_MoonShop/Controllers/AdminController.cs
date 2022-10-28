using KLTN_Web_MoonShop.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;

namespace KLTN_Web_MoonShop.Controllers
{
    public class AdminController : Controller
    {
        DBCosmeticEntities db = new DBCosmeticEntities();
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Product()
        {
            return View(db.Products.Where(n=>n.isActive==1).ToList());
        }
        public ActionResult AddProduct()
        {
                return PartialView();
        }
        [HttpPost]
        public ActionResult AddProduct(HttpPostedFileBase file,string txttensanpham,string txtsoluong,string txtgiathanh,string txtchitiet,string txthinhanh, string cbbloaisp)
        {
            try
            {
                //copy hình vào folder
                if (file.ContentLength > 0)
                {
                    string _FileName = Path.GetFileName(file.FileName);
                    string _path = Path.Combine(Server.MapPath("~/Asset/img/product"), _FileName);
                    file.SaveAs(_path);
                }
                Product pro = new Product();
                string proid = DateTime.Now.ToString("yyyyMMddHHmmss");
                pro.productID = long.Parse(proid);
                pro.productName = txttensanpham;
                pro.productImage =file.FileName;
                pro.productQuantity = int.Parse(txtsoluong);
                pro.productDescribe = txtchitiet;
                pro.productPrice = int.Parse(txtgiathanh);
                pro.productTypeID = int.Parse(cbbloaisp);
                pro.isActive = 1;
                db.Products.Add(pro);
                db.SaveChanges();
                return RedirectToAction("Product");
            }
            catch
            {
                return HttpNotFound();
            }
           
        }
        public ActionResult DeleteProduct(long id)
        {
            try
            {
                Product pro = db.Products.FirstOrDefault(n => n.productID.Equals(id));
                pro.isActive = 0;
                db.Products.AddOrUpdate(pro);
                db.SaveChanges();
                return RedirectToAction("Product");
            }
            catch
            {
                return HttpNotFound();
            }
        }

        public ActionResult UpdateProduct(long id)
        {
          if(id==0)
            {
                ViewBag.TitleProduct = "Thêm mới sản phẩm";
                return PartialView(db.Products.FirstOrDefault(n=>n.productID==id));
            }    
          else
            {
                ViewBag.TitleProduct = "Sửa sản phẩm";
                Product pro = db.Products.FirstOrDefault(n => n.productID.Equals(id));
                return PartialView(pro);
            }  
          
        }
        [HttpPost]
        public ActionResult UpdateProduct(HttpPostedFileBase file,string txtimg,long id, string txttensanpham, string txtsoluong, string txtgiathanh, string txtchitiet, string txthinhanh, string cbbloaisp)
        {
            bool action = false;
            int type = 0;
            try
            {
                //copy hình vào folder
                if(file!=null)
                {
                    string _FileName = Path.GetFileName(file.FileName);
                    string _path = Path.Combine(Server.MapPath("~/Asset/img/product"), _FileName);
                    file.SaveAs(_path);
                }    
                Product pro = new Product();
                if (id == 0)
                {
                    string proid = DateTime.Now.ToString("yyyyMMddHHmmss");
                    pro.productID = long.Parse(proid);
                    pro.productImage = file.FileName;
                }
                else
                {
                    type = 1;
                    pro.productID = id;
                    pro.productImage = txtimg;
                }    
                pro.productName = txttensanpham;
                pro.productQuantity = int.Parse(txtsoluong);
                pro.productDescribe = txtchitiet;
                pro.productPrice = int.Parse(txtgiathanh);
                pro.productTypeID = int.Parse(cbbloaisp);
                pro.isActive = 1;
                db.Products.AddOrUpdate(pro);
                db.SaveChanges();
                action = true;
                //lưu hành động
                if(action==true)
                {
                    ActionLog acl = new ActionLog();
                    string acid = DateTime.Now.ToString("yyyyMMddHHmmss");
                    acl.actionLogID = long.Parse(acid);
                    acl.actionDate = DateTime.Now;
                    acl.userName = "Admin";
                    //hành động update
                    if(type==1)
                    {
                        acl.idOject = id;
                        acl.actionName = "Sửa";
                    }
                    if (type == 0)
                    {
                        acl.actionName = "Thêm mới";
                    }
                    acl.nameTable = "Product";
                    acl.modun = "Admin/UpdateProduct";
                    db.ActionLogs.AddOrUpdate(acl);
                    db.SaveChanges();

                }    
                return RedirectToAction("Product");
            }
            catch
            {
                action = false;
                return HttpNotFound();
            }
           
        }

    }
}