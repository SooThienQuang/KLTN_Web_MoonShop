using KLTN_Web_MoonShop.Controllers.API;
using KLTN_Web_MoonShop.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
            Customer cus = db.Customers.FirstOrDefault(n => n.customerUserName.Equals(txt_email) && n.customerPassword.Equals(pass)&&n.isActive==1);
            if (cus!=null)
            {
                if(cus.isActive==1)
                {
                    Session["user"] = cus;
                    return RedirectToAction("Index", "Home");
                }    
                else
                {
                    ViewBag.LoginFail = "Tài khoản của bạn đã bị khóa!";
                }    
               
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
              
                if(db.Customers.FirstOrDefault(n=>n.customerUserName.Equals(phone))==null)
                {
                    Customer customer = new Customer();
                    long id = long.Parse(DateTime.Now.ToString("yyyyMMddHHmmss"));
                    customer.customerID = id;
                    customer.customerName = (firstname + " " + lastname);
                    customer.customerUserName = (phone).Trim();
                    customer.customerPassword = md5.CreateMD5(password);
                    customer.customerPhoto = "Sample_User_Icon.png".Trim();
                    customer.isActive = 1;
                    customer.dateCreate = DateTime.Now;
                    CustomerAddress cd = new CustomerAddress();
                    cd.ID = long.Parse(DateTime.Now.ToString("MMddHHmmssyyyy"));
                    cd.customerID = id;
                    cd.isActive = 1;
                    cd.isMain = 1;
                    cd.customerPhone = (phone).Trim();
                    db.CustomerAddresses.Add(cd);
                    db.Customers.Add(customer);
                    ViewBag.CreateSuccess = "Tạo tài khoản thành công";
                    //thông báo đăng kí thành công
                    Notification noti = new Notification();
                    long idnoti = long.Parse(DateTime.Now.ToString("yyyyMMddHHmmss"));
                    noti.notiID=idnoti;
                    noti.receiveUserID = id;
                    noti.receiveUserFullName = firstname + " " + lastname;
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
                    noti2.notiID = idnoti2+1;
                    noti2.receiveUserID = id;
                    noti2.receiveUserFullName = firstname + " " + lastname;
                    noti2.title = "Giảm giá cho khách hàng mới đến";
                    noti2.message = "Tặng bạn voucher giảm giá 20% : "+magiam;
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
                    dis.cusID = id;
                    dis.isActive = 1;
                    db.Discounts.Add(dis);
                    db.SaveChanges();
                  
                  
                  
                }    
                else
                {
                    ViewBag.CreateFail = "Tài khoản đã bị trùng vui lòng thử lại";
                }    
              
                return View();
            }
            catch
            {
                return View();
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
                ViewBag.quantityOrder = db.Orders.Where(n => n.customerID == cs.customerID).ToList().Count();
            }

            return PartialView();

        }
        public ActionResult DetailProfile(int id)
        {

            Customer cs = Session["user"] as Customer;
            if (cs != null)
            {
                string ten = cs.customerName.ToString().Split(' ').Last();
                ViewBag.name = ten;
                ViewBag.user = cs;
                ViewBag.useradd=db.CustomerAddresses.Where(n=>n.customerID==cs.customerID&&n.isActive==1).OrderByDescending(n=>n.isMain).ToList();
                ViewBag.menu = id;
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

            return View(cs);

        }
        public ActionResult Notifications()
        {

            Customer cs = Session["user"] as Customer;
            List<Notification> lst = new List<Notification>();
            if (cs != null)
            {
                string ten = cs.customerName.ToString().Split(' ').Last();
                ViewBag.name = ten;
                ViewBag.user = cs;
                ViewBag.quantityNotify= db.Notifications.Where(n => n.receiveUserID == cs.customerID).ToList().Count();
                lst = db.Notifications.Where(n => n.receiveUserID == cs.customerID).OrderByDescending(n => n.notiID).Take(5).ToList();
            }

            return PartialView(lst);

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
                      lst = db.CartDetails.Where(n => n.cartID == cart.cartID&&n.isActive==1).ToList();
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
                        ViewBag.lstCart = lsttam.Take(5);
                    }    
                }
                ViewBag.size = lst.Count;
            }
            else
            {
                if (Request.Cookies["CartCookie"] != null)
                {
                    List<cartTam> lstt = new List<cartTam>();
                    string chuoi = Request.Cookies["CartCookie"].Value.ToString();
                    string[] lst = chuoi.Split('|');
                    for (int i = 0; i < lst.Length; i++)
                    {
                        try
                        {
                            string[] l = lst[i].Split(',');
                            long proid = long.Parse(l[0]);
                            int quantity = int.Parse(l[1]);
                            cartTam c = new cartTam();
                            c.id = (long)proid;
                            Product pro = db.Products.ToList().FirstOrDefault(n => n.productID == proid);
                            c.name = pro.productName;
                            c.img = pro.productImage;
                            c.price = (long)pro.productPrice;
                            c.quantity = (long)quantity;
                            c.money = (long)pro.productPrice*quantity;
                            lstt.Add(c);
                        }
                        catch
                        {

                        }
                    }
                    ViewBag.lstCart = lstt.Take(5);
                    ViewBag.size = lstt.Count;
                }    
            }    

            return PartialView();

        }
        public ActionResult Order()
        {

            Customer cs = Session["user"] as Customer;
            List<Order> lst = new List<Order>();
            lst = db.Orders.Where(n => n.customerID == cs.customerID).OrderByDescending(n=>n.orderID).ToList();
            return PartialView(lst);

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
            return RedirectToAction("DetailProfile", "Customer", new {id=1});
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
                    cd.isActive = 1;
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
            return RedirectToAction("DetailProfile", "Customer", new {id=1});

        }
    }
}