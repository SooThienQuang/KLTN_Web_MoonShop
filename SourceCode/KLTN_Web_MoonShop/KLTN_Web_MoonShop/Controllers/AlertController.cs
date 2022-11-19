using KLTN_Web_MoonShop.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;

namespace KLTN_Web_MoonShop.Controllers
{
    public class AlertController : Controller
    {
        // GET: Alert
        DBCosmeticEntities db = new DBCosmeticEntities();
        MD5 md5 = new MD5();
        public ActionResult Success(string message)
        {
            ViewBag.message = message;
            return PartialView();
        }
        public ActionResult Fail(string message)
        {
            ViewBag.message = message;
            return PartialView();
        }

        public ActionResult NotLogin(string txtphone, string txtpws)
        {
            if (!txtphone.IsEmpty() && !txtpws.IsEmpty())
            {
                string pass = md5.CreateMD5(txtpws);
                Customer cus = db.Customers.FirstOrDefault(n => n.customerUserName.Equals(txtphone) && n.customerPassword.Equals(pass) || n.customerUserName.Equals(txtphone) && n.customerPassword.Equals(pass));
                if (cus != null)
                {
                    Session["user"] = cus;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.LoginFail = "Tài khoản hoặc mật khẩu chưa chính xác!";
                }

            }
            return RedirectToAction("Index", "Home");
        }
        public ActionResult Loader()
        {
            return PartialView();
        }
    }
}