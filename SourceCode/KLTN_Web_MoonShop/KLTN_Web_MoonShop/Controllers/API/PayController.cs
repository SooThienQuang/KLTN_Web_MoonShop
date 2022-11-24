using KLTN_Web_MoonShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace KLTN_Web_MoonShop.Controllers.API
{
    public class person
    {
        public long proID { get; set; }
        public long cusID { get; set; }
        public int price { get; set; }
        public int quantity { get; set; }
    }
    public class PayController : ApiController
    {
        DBCosmeticEntities db = new DBCosmeticEntities();
        [HttpPost]
        public string AddToCart(person obj)
        {
            try
            {
                Order order = new Order();
                long id = long.Parse(DateTime.Now.ToString("yyyyMMddHHmmss"));
                order.orderID = id;
                order.employeeID = id;
                order.customerID = obj.cusID;
                order.createDate = DateTime.Now;
                order.status = 1;
                db.Orders.Add(order);
                OrderDetail orderDetail = new OrderDetail();
                OrderDetail odlast = db.OrderDetails.ToList().LastOrDefault();
                if(odlast==null)
                {
                    orderDetail.orderDetailID = 0;
                }
                else
                {
                    orderDetail.orderDetailID=odlast.orderDetailID+1;
                }
                orderDetail.orderID=id;
                orderDetail.productID = obj.proID;
                orderDetail.Quantity = obj.quantity;
                orderDetail.price = obj.price;
                orderDetail.Money = orderDetail.Quantity * obj.price;
                //dia chi
                CustomerAddress cus = db.CustomerAddresses.FirstOrDefault(n => n.customerID == obj.cusID&&n.isMain==1);
                orderDetail.idAdd = cus.customerAdd;
                orderDetail.statusID = 1;
                db.OrderDetails.Add(orderDetail);
                //thêm thông báo đăt hàng thành công
                long idnoti = long.Parse(DateTime.Now.ToString("yyyyMMddHHmmss"));
                Notification noti = new Notification();
                Customer customer = db.Customers.FirstOrDefault(n => n.customerID == obj.cusID && n.isActive == 1);
                Product product = db.Products.FirstOrDefault(n => n.productID == obj.proID && n.isActive == 1);
                ProductDetail productDetail=db.ProductDetails.FirstOrDefault(n => n.ProductID == obj.proID);
                noti.notiID= idnoti;
                noti.receiveUserID = obj.cusID;
                noti.receiveUserFullName = customer.customerName;
                noti.title = "Bạn đã đặt hàng thành công !";
                noti.message = "Sản phẩm bao gồm :" + product.productName.Substring(0,30)+"..." + " (số lượng :" + obj.quantity + ")";
                noti.image = product.productImage;
                noti.menutype = 2;
                db.Notifications.Add(noti);
                db.SaveChanges();
                return "Đặt hàng thành công";
            }
            catch
            {
                return "lỗi hệ thống";
            }
            
        }
        
    }
}
