using KLTN_Web_MoonShop.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.EnterpriseServices;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;

namespace KLTN_Web_MoonShop.Controllers
{
    public class request
    {
        public string chuoi { get; set; }
    }

    public class PaymentController : Controller
    {
        DBCosmeticEntities db = new DBCosmeticEntities();
        // GET: Payment
        public ActionResult Checkout()
        {
            return View();
        }
        public ActionResult Detail()
        {
            return View();
        }
        public ActionResult Paymore(string id)
        {
            Customer user = Session["user"]as Customer;
            //id
            ViewBag.id = id;
            //gio hang
            Cart cart = db.Carts.FirstOrDefault(n => n.customerID == user.customerID);
            //
            ViewBag.cartID = cart.cartID;
            //danh sach tat ca chi tiet gio hang
            List<CartDetail> lstcartdeall = db.CartDetails.Where(n => n.cartID == cart.cartID && n.isActive == 1).ToList();
            //chi tiet san pham da chon tu gio hang
            List<CartDetail> lstd = new List<CartDetail>();
            List<Product> lst = new List<Product>();
            List<long> proid = new List<long>();
            if(id.Equals("getallcartqnt"))
            {
                if(user!=null)
                {
                    foreach(CartDetail detail in lstcartdeall)
                    {
                        Product d = db.Products.FirstOrDefault(n => n.productID == detail.productID);
                        lst.Add(d);
                    }    
                }
                lstd = lstcartdeall;
                ViewBag.CartDetail = lstd;
                ViewBag.allMoney = lstd.Sum(n => n.cartMoney);
            }
            else
            {
                string[] a = id.Split(',');
                var unique_items = new HashSet<string>(a);
                foreach (string s in unique_items)
                {
                    if (s != null && s.Equals("") == false)
                    {
                        proid.Add(long.Parse(s));
                    }
                }

                foreach (long pro in proid)
                {
                    Product pro1 = db.Products.FirstOrDefault(n => n.productID == pro);
                    lst.Add(pro1);
                   
                    foreach(var item in lstcartdeall)
                    {
                        if(item.productID==pro)
                        {
                            lstd.Add(item);
                        }    
                    }    
                }
                ViewBag.CartDetail = lstd;
                ViewBag.allMoney = lstd.Sum(n => n.cartMoney);
            }    
            
            return PartialView(lst);
        }
        public ActionResult Discount()
        {
            return PartialView();
        }
        public ActionResult pay(string id)
        {
            long productID = long.Parse(id);
            Product pro=db.Products.FirstOrDefault(n => n.productID == productID && n.isActive==1);
            List<Product> lst = new List<Product>();
            lst.Add(pro);
            return View(lst);
        }
        public ActionResult pays(string id)
        {
            
            Customer user = Session["user"] as Customer;
            Cart cart = db.Carts.FirstOrDefault(n => n.customerID == user.customerID);
            List<CartDetail> cartDetails = db.CartDetails.Where(n => n.cartID == cart.cartID).ToList();
            List<CartDetail> lst = new List<CartDetail>();
            string[] listid = id.Split(',');
            if(listid.Length>0)
            {
                ViewBag.lstProid = id;
                foreach(string item in listid)
                {
                    try
                    {
                        long proid = long.Parse(item);
                        lst.Add(cartDetails.FirstOrDefault(n => n.productID == proid));
                    }
                    catch
                    {

                    }
                }
            }
            return View(lst);
        }
        public ActionResult paynotlogin(long id)
        {
            List<Product> lstall = new List<Product>();
            if (id>0)
            {
                Product product = db.Products.FirstOrDefault(n => n.productID == id);
                product.productQuantity = 1;
                lstall.Add(product);
            }    
          else
            {
                if (Request.Cookies["CartCookie"] != null)
                {
                    string chuoi = Request.Cookies["CartCookie"].Value.ToString();
                    string[] lst = chuoi.Split('|');
                    for (int i = 0; i < lst.Length; i++)
                    {
                        try
                        {
                            string[] l = lst[i].Split(',');
                            long proid = long.Parse(l[0]);
                            int sl = int.Parse(l[1]);
                            Product peo2 = db.Products.FirstOrDefault(n => n.productID == proid);
                            peo2.productQuantity = sl;
                            lstall.Add(peo2);
                        }
                        catch
                        {

                        }
                    }
                }
                //Response.Cookies["CartCookie"].Value = null;
            }
            return View(lstall);
        }
        [HttpPost]
        public ActionResult paynotlogin(long id,string txthoten,string txtsdt,string txt,string txtstrsl,long txttongtien)
        {
            //tạo khách hàng
            Customer customer = new Customer();
            long id1 = long.Parse(DateTime.Now.ToString("yyyyMMddHHmmss"));
            customer.customerID = id1;
            customer.customerName = txthoten;
            customer.customerUserName = txtsdt;
            customer.isActive = -1;
            customer.dateCreate = DateTime.Now;
            CustomerAddress cd = new CustomerAddress();
            cd.ID = long.Parse(DateTime.Now.ToString("MMddHHmmssyyyy"));
            cd.customerID = id1;
            cd.customerAdd = txt;
            cd.isActive = 1;
            cd.isMain = 1;
            cd.customerPhone = txtsdt;
            db.CustomerAddresses.Add(cd);
            db.Customers.Add(customer);
            Order order = new Order();
            long orderid = long.Parse(DateTime.Now.ToString("yyyyMMddHHmmss"));
            order.orderID = orderid;
            order.employeeID = orderid;
            order.customerID = id1;
            order.createDate = DateTime.Now;
            order.status = 1;
            order.money = txttongtien;
            order.phone = txtsdt;
            db.Orders.Add(order);
            db.SaveChanges();
            string[] arrsl = txtstrsl.Split(',');
            if(id>0)
            {
                Product pro = db.Products.FirstOrDefault(n => n.productID == id);
                OrderDetail orderDetail = new OrderDetail();
                OrderDetail odlast = db.OrderDetails.ToList().LastOrDefault();
                if (odlast == null)
                {
                    orderDetail.orderDetailID = 0;
                }
                else
                {
                    orderDetail.orderDetailID = odlast.orderDetailID + 1;
                }
                orderDetail.orderID = orderid;
                orderDetail.productID = id;
                orderDetail.Quantity = int.Parse(arrsl[0]);
                orderDetail.price = pro.productPrice;
                orderDetail.Money = int.Parse(arrsl[0]) * pro.productPrice;
                orderDetail.idAdd = txt;
                orderDetail.statusID = 1;
                db.OrderDetails.Add(orderDetail);
                db.SaveChanges();
                long idnoti = long.Parse(DateTime.Now.ToString("yyyyMMddHHmmss"));
                Notification noti1 = new Notification();
                noti1.notiID = idnoti + 1;
                noti1.receiveGroupID = 1;
                noti1.receiveGroupName = "Quản trị";
                noti1.title = customer.customerName + " vừa đã đặt hàng!";
                noti1.message = "Tổng đơn hàng có " + 1 + " món " + "sản phẩm bao gồm :" + pro.productName.Substring(0, 30) + "...";
                noti1.image = pro.productImage;
                noti1.menutype = 2;
                noti1.isRead = 0;
                noti1.objectID = orderid;
                db.Notifications.Add(noti1);
                db.SaveChanges();
            }   
            else
            if (Request.Cookies["CartCookie"] != null)
            {
                string chuoi = Request.Cookies["CartCookie"].Value.ToString();
                string[] lst = chuoi.Split('|');
                for (int i = 0; i < lst.Length; i++)
                {
                    try
                    {
                        string[] l = lst[i].Split(',');
                        long proid = long.Parse(l[0]);
                        //tạo chi tiết đơn hàng
                        Product pro = db.Products.FirstOrDefault(n => n.productID == proid);
                        OrderDetail orderDetail = new OrderDetail();
                        OrderDetail odlast = db.OrderDetails.ToList().LastOrDefault();
                        if (odlast == null)
                        {
                            orderDetail.orderDetailID = 0;
                        }
                        else
                        {
                            orderDetail.orderDetailID = odlast.orderDetailID + 1;
                        }
                        orderDetail.orderID = orderid;
                        orderDetail.productID = proid;
                        orderDetail.Quantity = int.Parse(arrsl[i]);
                        orderDetail.price = pro.productPrice;
                        orderDetail.Money = int.Parse(arrsl[i]) * pro.productPrice;
                        orderDetail.idAdd = txt;
                        orderDetail.statusID = 1;
                        db.OrderDetails.Add(orderDetail);
                        db.SaveChanges();
                      
                    }
                    catch
                    { }

                }
                string[] l1 = lst[0].Split(',');
                long proid1 = long.Parse(l1[0]);
                Product proo = db.Products.FirstOrDefault(n => n.productID == proid1);
                long idnoti = long.Parse(DateTime.Now.ToString("yyyyMMddHHmmss"));
                Notification noti1 = new Notification();
                noti1.notiID = idnoti + 1;
                noti1.receiveGroupID = 1;
                noti1.receiveGroupName = "Quản trị";
                int lneg = lst.Length - 1;
                noti1.title = customer.customerName + " vừa đã đặt hàng!";
                noti1.message = "Tổng đơn hàng có " + lneg+ " món " + "sản phẩm bao gồm :" + proo.productName.Substring(0, 30) + "...";
                noti1.image = proo.productImage;
                noti1.menutype = 2;
                noti1.isRead = 0;
                noti1.objectID = orderid;
                db.Notifications.Add(noti1);
                db.SaveChanges();
                Response.Cookies["CartCookie"].Value = null;
            }
            Session["order"] = order;
            ViewBag.message = "Đặt hàng thành công";
            return RedirectToAction("index","home");
        }
    }
}