using KLTN_Web_MoonShop.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.Policy;
using System.Text;
using System.Web;
using System.Web.Mvc;
using static System.Net.WebRequestMethods;
using System.Runtime.Remoting.Contexts;

namespace KLTN_Web_MoonShop.Controllers
{
  public class cartTam
    {
        public long id { get; set; }
        public string name { get; set; }
        public long quantity { get; set; }
        public long price { get; set; }
        public string img { get; set; }
        public long money { get; set; }
    }
    public class CustomerController : Controller
    {
        DBCosmeticEntities db = new DBCosmeticEntities();
        MD5 md5 = new MD5();
     
        // GET: Customer
        public ActionResult Login()
        {
            Session["user"] = null;
            return View();
        }
        [HttpPost]
        public ActionResult Login(string txt_email,string txt_password)
        {
            if(txt_email.Equals("admin@gmail.com")&&txt_password.Equals("123"))
            {
                return RedirectToAction("Index", "Admin");
            }
            string pass = md5.CreateMD5(txt_password);
            Customer cus = db.Customers.FirstOrDefault(n => n.customerUserName.Equals(txt_email) && n.customerPassword.Equals(pass) || n.customerUserName.Equals(txt_email) && n.customerPassword.Equals(pass));
            if (cus!=null)
            {
                Session["user"] = cus;
                return RedirectToAction("Index", "Home");
            }   
            else
            {
                ViewBag.LoginFail = "Tài khoản hoặc mật khẩu chưa chính xác!";
            }
            return View();
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(string firstname,string lastname,string phone,string password,string repassword)
        {   
            if(!password.Equals(repassword))
            {
                ViewBag.ErrorPassword = "Lỗi nhập sai mật khẩu ? Vui lòng thử lại !";
            }
          try
            {
                Customer customer = new Customer();
                long id= long.Parse(DateTime.Now.ToString("yyyyMMddHHmmss"));
                customer.customerID = id;
                customer.customerName = (firstname + " " + lastname);
                customer.customerUserName = ("0"+phone).Trim();
                customer.customerPassword = md5.CreateMD5(password);
                customer.customerPhoto = "Sample_User_Icon.png".Trim();
                customer.isActive = 1;
                customer.dateCreate = DateTime.Now; 
                if(db.Customers.FirstOrDefault(n=>n.customerUserName.Equals(phone))==null)
                {
                    CustomerAddress cd = new CustomerAddress();
                    cd.ID = long.Parse(DateTime.Now.ToString("MMddHHmmssyyyy"));
                    cd.customerID = id;
                    cd.isActive = 1;
                    cd.isMain = 1;
                    cd.customerPhone = ("0" + phone).Trim();
                    db.CustomerAddresses.Add(cd);
                    db.Customers.Add(customer);
                    db.SaveChanges();
                    ViewBag.CreateSuccess = "Tạo tài khoản thành công";
                }    
                else
                {
                    ViewBag.CreateFail = "Tài khoản đã bị trùng vui lòng thử lại";
                }    
              
                return View();
            }
            catch
            {
                return RedirectToAction("Page404", "Error");
            }
        }


        //----------------------------------------------------navbar----------------------------------
        public ActionResult Profile()
        {

            Customer cs = Session["user"] as Customer;
            if (cs != null)
            {
                string ten = cs.customerName.ToString().Split(' ').Last();
                ViewBag.name = ten;
                ViewBag.user = cs;
            }

            return PartialView();

        }
        public ActionResult DetailProfile()
        {

            Customer cs = Session["user"] as Customer;
            if (cs != null)
            {
                string ten = cs.customerName.ToString().Split(' ').Last();
                ViewBag.name = ten;
                ViewBag.user = cs;
                ViewBag.useradd=db.CustomerAddresses.Where(n=>n.customerID==cs.customerID).ToList();
            }

            return View(cs);

        }
        [HttpPost]
        public ActionResult DetailProfile(HttpPostedFileBase imguser)
        {
            if (imguser.ContentLength > 0)
            {
                string _FileName = Path.GetFileName(imguser.FileName);
                string _path = Path.Combine(Server.MapPath("~/Asset/img/user"), _FileName);
                imguser.SaveAs(_path);
            }
            Customer cs = Session["user"] as Customer;
            if (cs != null)
            {
                string ten = cs.customerName.ToString().Split(' ').Last();
                ViewBag.name = ten;
                ViewBag.user = cs;
            }

            return View();

        }
        public ActionResult Notification()
        {

            Customer cs = Session["user"] as Customer;
            if (cs != null)
            {
                string ten = cs.customerName.ToString().Split(' ').Last();
                ViewBag.name = ten;
                ViewBag.user = cs;
            }

            return PartialView();

        }
        public void RefreshAll()
        {
            this.db = new DBCosmeticEntities();
        }
        public ActionResult Cart()
        {
           int sl= db.Carts.Count();
            db = new DBCosmeticEntities();
            Customer cs = Session["user"] as Customer;
            if (cs != null)
            {
                string ten = cs.customerName.ToString().Split(' ').Last();
                ViewBag.name = ten;
                ViewBag.user = cs;
                Cart cart = new Cart();
                cart=db.Carts.ToList().FirstOrDefault(n=>n.customerID==cs.customerID);
                List<CartDetail> lst = new List<CartDetail>();
                if(cart!=null)
                {
                      lst = db.CartDetails.Where(n => n.cartID == cart.cartID).ToList();
                    List<cartTam> lsttam = new List<cartTam>();
                     if(lst.Count>0)
                    {
                        foreach (var item in lst)
                        {
                            cartTam c = new cartTam();
                            c.id = (long)item.productID;
                            Product pro = db.Products.ToList().FirstOrDefault(n => n.productID == item.productID);
                            c.name = pro.productName;
                            c.img = pro.productImage;
                            c.price = (long)pro.productPrice;
                            c.quantity = (long)item.cartQuantity;
                            c.money = (long)item.cartMoney;
                            lsttam.Add(c);
                            
                        }
                        ViewBag.AllMoney = lst.Sum(n => n.cartMoney);
                        ViewBag.lstCart = lsttam;
                    }    
                }
                ViewBag.size = lst.Count;
            }

            return PartialView();

        }
        public ActionResult Order()
        {

            Customer cs = Session["user"] as Customer;
            if (cs != null)
            {
                string ten = cs.customerName.ToString().Split(' ').Last();
                ViewBag.name = ten;
                ViewBag.user = cs;
            }

            return PartialView();

        }
        public ActionResult img()
        {
            return PartialView();

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
            
            if(cs!=null)
            {
                cs.customerPhoto = image.FileName;
                db.Customers.AddOrUpdate(cs);
                db.SaveChanges();
                Session["user"]=cs;
            }
            return RedirectToAction("DetailProfile");
        }


        public ActionResult Address()
        {
            return PartialView();

        }
        [HttpPost]
        public ActionResult Address(string txt,string txtduong)
        {
            Customer cs = Session["user"] as Customer;

            if (cs != null)
            {
                CustomerAddress cd = db.CustomerAddresses.FirstOrDefault(n => n.customerID == cs.customerID);
                if(cd.customerAdd==null)
                {
                    cd.customerAdd = txtduong + "," + txt;
                    db.CustomerAddresses.AddOrUpdate(cd);
                    db.SaveChanges();
                }
                else
                {
                    CustomerAddress cd1 = new CustomerAddress();
                    cd1.ID = long.Parse(DateTime.Now.ToString("MMddHHmmssyyyy"));
                    cd1.customerID = cs.customerID;
                   // cd1.customerPhone = cs.customerUserName;
                    cd1.customerAdd= txtduong + "," + txt;
                    cd1.isMain = 0;
                    cd1.isActive = 1;
                    db.CustomerAddresses.Add(cd1);
                    db.SaveChanges();
                }    
             
              
                Session["user"] = cs;
            }
            return RedirectToAction("DetailProfile");

        }
    }
}