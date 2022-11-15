using System;
using System.Collections.Generic;
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
    }
    public class order
    {

        public int phone { get; set; }
        public DateTime date { get; set; }
    }
    public class OrderController : ApiController
    {
        public order get(long orderid,long proid)
        {
            order d = new order();
            return d;
        }
    }
}
