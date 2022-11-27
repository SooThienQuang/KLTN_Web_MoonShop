using KLTN_Web_MoonShop.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace KLTN_Web_MoonShop.Controllers.API
{
    public class orderRequest
    {
        public long orderID { get; set; }
        public long proID { get; set; }
        public int status { get; set; }
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
                Order order = db.Orders.FirstOrDefault(n => n.orderID == d.orderID);
                order.status = d.status;
                db.Orders.AddOrUpdate();
                db.SaveChanges();
                return "Thành công";
            }
            catch
            {
                return "Thất bại";
            }
        }
    }
}
