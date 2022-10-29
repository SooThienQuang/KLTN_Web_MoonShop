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
                ViewBag.VisibleButton = true;
                ViewBag.TitleProduct = "Thêm mới sản phẩm";
                ViewBag.ProductType = db.ProductTypes.Where(n=>n.isActive==1).ToList();
                return PartialView(db.Products.FirstOrDefault(n=>n.productID==id));
            }   
          if(id==1)
            {
                ViewBag.TitleProduct = "Chi tiết sản phẩm";
                ViewBag.VisibleButton = false;
                ViewBag.ProductType = db.ProductTypes.Where(n => n.isActive == 1).ToList();
                return PartialView(db.Products.FirstOrDefault(n => n.productID == id));
            }    
          else
            {
                ViewBag.VisibleButton = true;
                ViewBag.TitleProduct = "Sửa sản phẩm";
                ViewBag.ProductType = db.ProductTypes.Where(n=>n.isActive==1).ToList();
                Product pro = db.Products.FirstOrDefault(n => n.productID.Equals(id));
                ViewBag.ProductDetail = db.ProductDetails.FirstOrDefault(n => n.ProductID.Equals(id));
                return PartialView(pro);
            }  
          
        }
        [HttpPost]
        public ActionResult UpdateProduct(HttpPostedFileBase file,string txtxuatxu, string txtsize, string txtcolor, HttpPostedFileBase img1, HttpPostedFileBase img2, HttpPostedFileBase img3, string txtimg, string txtimg1, string txtimg2, string txtimg3, long id, string txttensanpham, string txtsoluong, string txtgiathanh, string txtchitiet, string txthinhanh, string cbbloaisp)
        {
            bool action = false;
            int type = 0;
            try
            {
                //copy hình vào folder
                if(file!=null&&img1!=null&&img2!=null&&img3!=null)
                {
                    string _FileName = Path.GetFileName(file.FileName);
                    string _path = Path.Combine(Server.MapPath("~/Asset/img/product"), _FileName);
                    file.SaveAs(_path);

                    string _FileName1 = Path.GetFileName(img1.FileName);
                    string _path1 = Path.Combine(Server.MapPath("~/Asset/img/product"), _FileName1);
                    img1.SaveAs(_path1);

                    string _FileName2 = Path.GetFileName(img2.FileName);
                    string _path2 = Path.Combine(Server.MapPath("~/Asset/img/product"), _FileName2);
                    img2.SaveAs(_path2);

                    string _FileName3 = Path.GetFileName(img3.FileName);
                    string _path3 = Path.Combine(Server.MapPath("~/Asset/img/product"), _FileName3);
                    img3.SaveAs(_path3);
                }    
                Product pro = new Product();
                ProductDetail prod = new ProductDetail();
                //thêm mới
                if (id == 0)
                {
                    string proid = DateTime.Now.ToString("yyyyMMddHHmmss");
                    pro.productID = long.Parse(proid);
                    pro.productImage = file.FileName;

                    prod.ProductID = long.Parse(proid);
                    prod.img1 = img1.FileName;
                    prod.img2 = img2.FileName;
                    prod.img3 = img3.FileName;                  
                }
                else
                {
                    //update
                    type = 1;
                    pro.productID = id;
                    prod.ProductID = id;
                    
                    if(file==null)
                    {
                        pro.productImage = txtimg;
                    }    
                    else
                    {
                        pro.productImage = file.FileName;
                    }
                   
                    if(img1==null)
                    {
                        prod.img1 = txtimg1;
                    }    
                    else
                    {
                        prod.img1 = img1.FileName;
                    }    
                  
                    if(img2==null)
                    {
                        prod.img2 = txtimg2;
                    }    
                    else
                    {
                        prod.img2 = img2.FileName;
                    }    
                  if(img3==null)
                    {
                        prod.img3 = txtimg3;
                    }    
                  else
                    {
                        prod.img3 = img3.FileName;
                    }    
                   
                }    
                pro.productName = txttensanpham;
                pro.productQuantity = int.Parse(txtsoluong);
                pro.productDescribe = txtchitiet;
                pro.productPrice = int.Parse(txtgiathanh);
                pro.productTypeID = int.Parse(cbbloaisp);
                pro.isActive = 1;

                prod.Size = txtsize;
                prod.Color = txtcolor;
                prod.origin = txtxuatxu;
                db.ProductDetails.AddOrUpdate(prod);
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