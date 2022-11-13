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
                Order last=db.Orders.FirstOrDefault(n=>n.customerID==obj.cusID);
                long id = 0;
                Order order = new Order();
                if (last==null)
                {
                    id = long.Parse(DateTime.Now.ToString("yyyyMMddHHmmss"));
                    order.orderID = id;
                    order.employeeID = id;
                    order.customerID = obj.cusID;
                    order.createDate = DateTime.Now;
                    order.isActive = 1;
                    db.Orders.Add(order);
                }
                else
                {
                    id = last.orderID;
                }
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
