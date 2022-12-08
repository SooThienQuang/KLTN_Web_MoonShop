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
    public class DiscountController : ApiController
    {
        public class discount
        {
            public string code { get; set; }
            public string name { get; set; }
            public string img { get; set; }
            public int percent { get; set; }
        }
        DBCosmeticEntities db = new DBCosmeticEntities();
        [HttpPost]
        public discount getdiscount(discount code)
        {
            var item = db.Discounts.FirstOrDefault(n => n.code.Equals(code.code)&&n.isActive==1);
            if(item==null)
            {
                return null;
            }    
            else
            {
                discount d = new discount();
                d.code = code.code;
                d.percent = (int)item.percentDiscount;
                d.name = item.name;
                d.img = item.image;
                return d;
            }    
           
        }
        [HttpPut]
        public bool putdiscount(discount code)
        {
           try
            {
                var item = db.Discounts.FirstOrDefault(n => n.code.Equals(code.code) && n.isActive == 1);
                item.isActive = -1;
                db.Discounts.AddOrUpdate(item);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }
    }

}
