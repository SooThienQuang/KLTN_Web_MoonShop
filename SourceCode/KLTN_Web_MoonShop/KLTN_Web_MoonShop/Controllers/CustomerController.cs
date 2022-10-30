using KLTN_Web_MoonShop.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.Policy;
using System.Text;
using System.Web;
using System.Web.Mvc;
namespace KLTN_Web_MoonShop.Controllers
{
    public class CustomerController : Controller
    {
        DBCosmeticEntities db = new DBCosmeticEntities();
        MD5 md5 = new MD5();
        // GET: Customer
        public ActionResult Login()
        {
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
            Customer cus = db.Customers.FirstOrDefault(n => n.customerEmail.Equals(txt_email) && n.customerPassword.Equals(pass));
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
        public ActionResult Register(string firstname,string lastname,string email,string phone,string password,string repassword)
        {   
            if(!password.Equals(repassword))
            {
                ViewBag.ErrorPassword = "Lỗi nhập sai mật khẩu ? Vui lòng thử lại !";
            }
          try
            {
                Customer customer = new Customer();
                customer.customerID =long.Parse(DateTime.Now.ToString("yyyyMMddHHmmss"));
                customer.customerName = (firstname + "" + lastname).Trim();
                customer.customerEmail = email.Trim();
                customer.customerSex = "";
                customer.customerAddress = "";
                customer.customerUserName = phone.Trim();
                customer.customerPassword = md5.CreateMD5(password);
                customer.customerPhoto = "sothienquang.jpg";
                customer.isActive = 1;
                customer.dateCreate = DateTime.Now;  
                if(db.Customers.FirstOrDefault(n=>n.customerEmail.Equals(email))==null)
                {
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
    }
}