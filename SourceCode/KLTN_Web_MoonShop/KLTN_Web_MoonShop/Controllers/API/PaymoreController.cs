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
    }
    public class PaymoreController : ApiController
    {
        DBCosmeticEntities db = new DBCosmeticEntities();
        [HttpPost]
        public string paymore(request data)
        {
            List<CartDetail> lstcartdeall = db.CartDetails.Where(n => n.cartID == data.cartID && n.isActive == 1).ToList();
            if (data.lstod.Equals("getallcartqnt"))
            {
                Order order = new Order();
                long id = long.Parse(DateTime.Now.ToString("yyyyMMddHHmmss"));
                order.orderID = id;
                order.employeeID = id;
                order.customerID = data.cusID;
                order.createDate = DateTime.Now;
                order.status = 1;
                db.Orders.Add(order);
                db.SaveChanges();
                foreach (CartDetail detail in lstcartdeall)
                {
                    detail.isActive = -1;
                    db.CartDetails.AddOrUpdate(detail);
                    db.SaveChanges();
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
                    orderDetail.orderID = id;
                    orderDetail.productID = detail.productID;
                    orderDetail.Quantity = detail.cartQuantity;
                    Product pro = db.Products.FirstOrDefault(n => n.productID == detail.productID);
                    orderDetail.price = pro.productPrice;
                    orderDetail.Money = orderDetail.Quantity * pro.productPrice;
                    //dia chi
                    CustomerAddress cus = db.CustomerAddresses.FirstOrDefault(n => n.customerID == data.cusID && n.isMain == 1);
                    orderDetail.idAdd = cus.customerAdd;
                    orderDetail.statusID = 1;
                    db.OrderDetails.Add(orderDetail);
                    db.SaveChanges();

                }
               
                return "thanh cong";
            }
            else
            {
                List<long> proid = new List<long>();
                string[] a = data.lstod.Split(',');
                var unique_items = new HashSet<string>(a);
                foreach (string s in unique_items)
                {
                    if (s != null && s.Equals("") == false)
                    {
                        proid.Add(long.Parse(s));
                    }
                }
                Order order = new Order();
                long id = long.Parse(DateTime.Now.ToString("yyyyMMddHHmmss"));
                order.orderID = id;
                order.employeeID = id;
                order.customerID = data.cusID;
                order.createDate = DateTime.Now;
                order.status = 1;
                db.Orders.Add(order);
                db.SaveChanges();
                foreach (long idn in proid)
                {
                    CartDetail cd = lstcartdeall.FirstOrDefault(n => n.productID == idn);
                    cd.isActive = -1;
                    db.CartDetails.AddOrUpdate(cd);
                    db.SaveChanges();
                    if(cd!=null)
                    {
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
                        orderDetail.orderID = id;
                        orderDetail.productID = idn;
                        orderDetail.Quantity = cd.cartQuantity;
                        Product pro = db.Products.FirstOrDefault(n => n.productID == idn);
                        orderDetail.price = pro.productPrice;
                        orderDetail.Money = orderDetail.Quantity * pro.productPrice;
                        //dia chi
                        CustomerAddress cus = db.CustomerAddresses.FirstOrDefault(n => n.customerID == data.cusID && n.isMain == 1);
                        orderDetail.idAdd = cus.customerAdd;
                        orderDetail.statusID = 1;
                        db.OrderDetails.Add(orderDetail);
                        db.SaveChanges();
                    }    
                }    
                return "thanh cong";
            }    
        }
    }
}
