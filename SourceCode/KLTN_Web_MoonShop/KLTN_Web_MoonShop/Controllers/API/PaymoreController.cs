using KLTN_Web_MoonShop.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace KLTN_Web_MoonShop.Controllers.API
{
    public class request
    {
        public long cusID { get; set; }
         public long cartID { get; set; }
        public string lstod { get;set; }
        public long money { get; set; }
    }
    public class PaymoreController : ApiController
    {
        DBCosmeticEntities db = new DBCosmeticEntities();
        [HttpPost]
        public string paymore(request data)
        {
            //tất cả cart của user
            Cart cart = db.Carts.FirstOrDefault(n => n.customerID == data.cusID);
            List<CartDetail> lstcd = db.CartDetails.Where(n => n.cartID == cart.cartID).ToList();
            //lấy tất cả product
            List<Product> lstproductall = db.Products.Where(n => n.isActive == 1).ToList();
            string[] lstid = data.lstod.Split(',');
            if(lstid.Length>0)
            {
                //tạo đơn hàng
                Order order = new Order();
                long orderid = long.Parse(DateTime.Now.ToString("yyyyMMddHHmmss"));
                order.orderID = orderid;
                order.employeeID = orderid;
                order.customerID = data.cusID;
                order.createDate = DateTime.Now;
                order.status = 1;
                order.money = data.money;
                db.Orders.Add(order);
                db.SaveChanges();
                foreach (string id in lstid)
                {
                   
                    try
                    {
                        long proid = long.Parse(id);
                        CartDetail cd = lstcd.FirstOrDefault(n => n.productID == proid);
                        Product pro = lstproductall.FirstOrDefault(n => n.productID == proid);
                        pro.productQuantity = pro.productQuantity - cd.cartQuantity;
                        //update product
                        db.Products.AddOrUpdate(pro);
                        db.SaveChanges();
                        //update cart
                        cd.isActive = -1;
                        db.CartDetails.AddOrUpdate(cd);
                        db.SaveChanges();
                        //tạo chi tiết đơn hàng
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
                        orderDetail.productID =proid;
                        orderDetail.Quantity =cd.cartQuantity;
                        orderDetail.price =pro.productPrice;
                        orderDetail.Money = cd.cartQuantity* pro.productPrice;
                        //dia chi
                        CustomerAddress cus = db.CustomerAddresses.FirstOrDefault(n => n.customerID == data.cusID && n.isMain == 1);
                        orderDetail.idAdd = cus.customerAdd;
                        orderDetail.statusID = 1;
                        db.OrderDetails.Add(orderDetail);
                        db.SaveChanges();

                    }
                    catch
                    {

                    }
                }
              try
                {
                    //thêm thông báo đăt hàng thành công
                    long proidone = long.Parse(lstid.FirstOrDefault());
                    long idnoti = long.Parse(DateTime.Now.ToString("yyyyMMddHHmmss"));
                    Notification noti = new Notification();
                    Customer customer = db.Customers.FirstOrDefault(n => n.customerID == data.cusID && n.isActive == 1);
                    Product product = db.Products.FirstOrDefault(n => n.productID == proidone && n.isActive == 1);
                    ProductDetail productDetail = db.ProductDetails.FirstOrDefault(n => n.ProductID == proidone);
                    noti.notiID = idnoti;
                    noti.receiveUserID = data.cusID;
                    noti.receiveUserFullName = customer.customerName;
                    noti.title = "Bạn đã đặt hàng thành công !";
                    noti.message ="Tổng đơn hàng có "+lstid.Count()+" món "+ "sản phẩm bao gồm :" + product.productName.Substring(0, 30) + "...";
                    noti.image = product.productImage;
                    noti.menutype = 2;
                    noti.isRead = 0;
                    noti.objectID = orderid;
                    db.Notifications.Add(noti);
                    db.SaveChanges();
                    Notification noti1 = new Notification();
                    noti1.notiID = idnoti+1;
                    noti1.receiveGroupID = 1;
                    noti1.receiveGroupName ="Quản trị";
                    noti1.title = customer.customerName+ " vừa đã đặt hàng!";
                    noti1.message = "Tổng đơn hàng có " + lstid.Count() + " món " + "sản phẩm bao gồm :" + product.productName.Substring(0, 30) + "...";
                    noti1.image = product.productImage;
                    noti1.menutype = 2;
                    noti1.isRead = 0;
                    noti1.objectID = orderid;
                    db.Notifications.Add(noti1);
                    db.SaveChanges();

                }
                catch { }
            }    
            return "Thành công";
        }
    }
}
