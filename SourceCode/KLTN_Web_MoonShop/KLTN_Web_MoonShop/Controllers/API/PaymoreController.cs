using KLTN_Web_MoonShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace KLTN_Web_MoonShop.Controllers.API
{
    public class request
    {
        public string chuoi { get; set; }
    }
    public class PaymoreController : ApiController
    {
        DBCosmeticEntities db = new DBCosmeticEntities();
        [HttpPost]
        public List<Product> paymore(request data)
        {
            List<Product> lst = new List<Product>();
            //Product result = db.Products.FirstOrDefault();
            //lst.Add(result);
            return db.Products.ToList(); 
        }
    }
}
