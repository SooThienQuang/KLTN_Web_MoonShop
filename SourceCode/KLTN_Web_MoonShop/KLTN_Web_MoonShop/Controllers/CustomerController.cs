using KLTN_Web_MoonShop.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KLTN_Web_MoonShop.Controllers
{
    public class CustomerController : Controller
    {
        DBCosmeticEntities db = new DBCosmeticEntities();
        // GET: Customer
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string txt_email)
        {
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
                customer.customerName = firstname + "" + lastname;
                customer.customerEmail = email;
                customer.customerSex = "";
                customer.customerAddress = "";
                customer.customerUserName = phone;
                customer.customerPassword = password;
                customer.customerPhoto = "sothienquang.jpg";
                customer.isActive = 1;
                customer.dateCreate = DateTime.Now;
                db.Customers.AddOrUpdate(customer);
                db.SaveChanges();
                ViewBag.CreateSuccess = "Tạo tài khoản thành công";
                return View();
            }
            catch
            {
                return RedirectToAction("Page404", "Error");
            }
        }
    }
}