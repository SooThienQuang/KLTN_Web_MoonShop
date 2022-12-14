using KLTN_Web_MoonShop.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Security.Policy;
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
            long idod = long.Parse(DateTime.Now.ToString("yyyyMMdd") + "120000");
            ViewBag.idod=idod;
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string username,string password)
        {
            if (username.Length == 0 || password.Length == 0)
                ViewBag.message = "Vui lòng nhập tài khoản mật khẩu";
            string pass = md5.CreateMD5(password);
            Employee em2=db.Employees.Where(n=>n.Password==pass&&n.UserName==username).FirstOrDefault();
            if (username.Equals("admin@gmail.com") && password.Equals("123"))
            {
                Employee em = new Employee();
                em.UserName = "admin";
                Session["employee"] = em;
                return RedirectToAction("index");
            }
            if(em2!=null)
            {
                EmployeeDetail emd = db.EmployeeDetails.FirstOrDefault(n => n.emID == em2.emID && n.isActive == 1);
                Session["employee"] = emd;
                return RedirectToAction("index");
            }
            else
            {
                ViewBag.message = "Tài khoản hoặc mật khẩu không chính xác";
                return View();
            }
        }
        public ActionResult Product()
        {
            return View(db.Products.Where(n=>n.isActive==1).OrderByDescending(n=>n.productID).ToList());
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
                ViewBag.ProductType = db.ProductTypes.Where(n=>n.isActive==1).OrderBy(n=>n.proTypeName).ToList();
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
                ViewBag.ProductType = db.ProductTypes.Where(n=>n.isActive==1).OrderBy(n => n.proTypeName).ToList();
                Product pro = db.Products.FirstOrDefault(n => n.productID.Equals(id));
                ViewBag.ProductDetail = db.ProductDetails.FirstOrDefault(n => n.ProductID.Equals(id));
                return PartialView(pro);
            }  
          
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult UpdateProduct(string txtmotachitiet, HttpPostedFileBase file,string txtxuatxu, string txtsize, string txtcolor, HttpPostedFileBase img1, HttpPostedFileBase img2, HttpPostedFileBase img3, string txtimg, string txtimg1, string txtimg2, string txtimg3, long id, string txttensanpham, string txtsoluong, string txtgiathanh, string txtchitiet, string txthinhanh, string cbbloaisp)
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
                if ( img1 != null)
                {
                    string _FileName1 = Path.GetFileName(img1.FileName);
                    string _path1 = Path.Combine(Server.MapPath("~/Asset/img/product"), _FileName1);
                    img1.SaveAs(_path1);
                }
                if (img2 != null)
                {
                    string _FileName2 = Path.GetFileName(img2.FileName);
                    string _path2 = Path.Combine(Server.MapPath("~/Asset/img/product"), _FileName2);
                    img2.SaveAs(_path2);
                }
                if (img3 != null)
                {
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
                prod.desciption = txtmotachitiet;
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
        //-----------------------------------------------quan ly khach hàng------------------------------------
        public ActionResult Customer()
        {
            return View(db.Customers.OrderByDescending(n=>n.isActive).ToList());
        }

        public ActionResult CRUDCus(long id)
        {
            Customer cus = new Customer();
            if (id!=0)
            {
                cus =  db.Customers.FirstOrDefault(n => n.customerID == id);
            }    
         
            return View(cus);
        }
        MD5 md5 = new MD5();
        [HttpPost]
        public ActionResult CRUDCus(Customer cus, HttpPostedFileBase img)
        {
            if (img!=null)
            {
                string _FileName = Path.GetFileName(img.FileName);
                string _path = Path.Combine(Server.MapPath("~/Asset/img/user"), _FileName);
                img.SaveAs(_path);
                cus.customerPhoto = _FileName;
            }
            if(cus.customerID!=0)
            {
                Customer old = db.Customers.FirstOrDefault(n => n.customerID == cus.customerID);
                if (!old.customerPassword.Equals(cus.customerPassword))
                {
                    string pass = md5.CreateMD5(cus.customerPassword);
                    cus.customerPassword = pass;
                }
            }
            else
            {
                string acid = DateTime.Now.ToString("yyyyMMddHHmmss");
                long cusID= long.Parse(acid);
                cus.customerID = long.Parse(acid);
                string pass = md5.CreateMD5(cus.customerPassword);
                cus.customerPassword = pass;
                cus.dateCreate = DateTime.Now;
                CustomerAddress cd = new CustomerAddress();
                cd.ID = long.Parse(DateTime.Now.ToString("MMddHHmmssyyyy"));
                cd.customerID = cusID;
                cd.isActive = 1;
                cd.isMain = 1;
                cd.customerPhone = cus.customerUserName;
                db.CustomerAddresses.Add(cd);
                //thông báo đăng kí thành công
                Notification noti = new Notification();
                long idnoti = long.Parse(DateTime.Now.ToString("yyyyMMddHHmmss"));
                noti.notiID = idnoti;
                noti.receiveUserID = cusID;
                noti.receiveUserFullName = cus.customerName;
                noti.title = "Chúc mừng bạn đã đăng kí tài khoản thành công";
                noti.message = "Cám ơn bạn đã tin tưởng Shop , mong bạn có một trải nghiệm tốt bằng các dịch vụ của chúng mình !";
                noti.image = "check.jpg";
                noti.menutype = 1;
                noti.isRead = 0;
                db.Notifications.Add(noti);
                db.SaveChanges();
                //tạo mã giảm
                string magiam = RandomUniCode.RandomString(12);

                //thông báo giảm giá khách hàng mới
                Notification noti2 = new Notification();
                long idnoti2 = long.Parse(DateTime.Now.ToString("yyyyMMddHHmmss"));
                noti2.notiID = idnoti2;
                noti2.receiveUserID = cusID;
                noti2.receiveUserFullName = cus.customerName;
                noti2.title = "Giảm giá cho khách hàng mới đến";
                noti2.message = "Tặng bạn voucher giảm giá 20% : " + magiam;
                noti2.image = "discount20.jpg";
                noti2.menutype = 1;
                noti2.isRead = 0;
                db.Notifications.Add(noti2);
                db.SaveChanges();
                Discount dis = new Discount();
                dis.ID = idnoti2;
                dis.name = noti2.title;
                dis.code = magiam;
                dis.percentDiscount = 10;
                dis.cusID = cusID;
                dis.isActive = 1;
                db.Discounts.Add(dis);
                db.SaveChanges();
            }
            cus.dateCreate = DateTime.Now;
            cus.isActive = 1;
            db.Customers.AddOrUpdate(cus);
            db.SaveChanges();
         
            return RedirectToAction("Customer");
        }
        public ActionResult CaroselHome()
        {
            var img = db.images.Where(n => n.type == 0&&n.isActive==1).ToList();
            image im= img.FirstOrDefault();
            ViewBag.active = im;
            return View(img);
        }
        public ActionResult imageproduct()
        {
            return View();
        }
        public ActionResult notify()
        {
            List<Notification> lst = db.Notifications.Where(n => n.receiveGroupName != null).ToList();
            return View(lst);
        }
    }
}