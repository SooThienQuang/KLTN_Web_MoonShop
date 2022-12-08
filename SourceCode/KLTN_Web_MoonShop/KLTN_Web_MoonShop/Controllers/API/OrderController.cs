using KLTN_Web_MoonShop.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Configuration;
using System.Web.Http;


namespace KLTN_Web_MoonShop.Controllers.API
{
    public class orderRequest
    {
        public long orderID { get; set; }
        public long proID { get; set; }
        public int status { get; set; }
        public string cbbshippingname { get; set; }
        public DateTime senddate { get; set; }
        public DateTime finishdate { get; set; }
        public long receiveID { get; set; }
        public long objetID { get; set; }

    }
    public class order
    {

        public int phone { get; set; }
        public DateTime date { get; set; }
    }
    public class OrderController : ApiController
    {
        DBCosmeticEntities db = new DBCosmeticEntities();
        public order get(long orderid,long proid)
        {
            order d = new order();
            return d;
        }
        [HttpPut]
        public string put(orderRequest d)
        {
          try
            {
                long id = long.Parse(DateTime.Now.ToString("yyyyMMddHHmmss"));
                Order order = db.Orders.FirstOrDefault(n => n.orderID == d.orderID);
                List<OrderDetail> lstd = db.OrderDetails.Where(n => n.orderID == d.orderID).ToList();
                OrderDetail d1 = lstd.FirstOrDefault();
                Product pro = db.Products.FirstOrDefault(n => n.isActive == 1 && n.productID == d1.productID);
                Customer customer = db.Customers.FirstOrDefault(n => n.customerID == order.customerID);
                order.status = d.status;
                db.Orders.AddOrUpdate();
                db.SaveChanges();
                ProcessOrder ps = new ProcessOrder();
                ps.processID = id;
                ps.objectID = d.orderID;
                ps.shippingName=d.cbbshippingname;
                ps.receiveUserID = d.receiveID;
                ps.sendDate = d.senddate;
                ps.finishDate = d.finishdate;
                ps.processType = 0;
                ps.description = "";
                db.ProcessOrders.Add(ps);
                db.SaveChanges();

                //gửi thông báo
                Notification noti = new Notification();
                long idnoti = long.Parse(DateTime.Now.ToString("yyyyMMddHHmmss"));
                noti.notiID = idnoti;
                noti.receiveUserID = customer.customerID;
                noti.receiveUserFullName =customer.customerName;
                noti.title = "Đơn hàng #"+order.orderID+" của bạn đã được "+getStatus(d.status,d.cbbshippingname);
                noti.message = "Có "+lstd.Count()+" sản phẩm gồm :"+pro.productName.Substring(0,20)+"...";
                noti.image =pro.productImage;
                noti.menutype = 2;
                noti.isRead = 0;
                db.Notifications.Add(noti);
                db.SaveChanges();
                return "Thành công";
            }
            catch
            {
                return "Thất bại";
            }
        }
        public string getStatus(int id,string shippingname)
        {
            string name = "Chờ xác nhận";
            if(id==2)
            {
                name = "đã được xác nhận";
            }   
            if(id==3)
            {
                name = "đang được vận chuyển bởi đơn vị " + shippingname;
            }    
            if(id==4)
            {
                name = "đơn hàng của bạn đã được giao thành công lúc :"+ DateTime.Now.ToString();
            }
            return name;
        }
    }
}
