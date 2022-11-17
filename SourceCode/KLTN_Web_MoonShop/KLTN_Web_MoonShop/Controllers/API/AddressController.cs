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
    public class address
    {
        public long id { get;set; }
        public long cusID { get; set; }
    }
    public class AddressController : ApiController
    {
        DBCosmeticEntities db = new DBCosmeticEntities();
        [HttpDelete]
        public string del(address d)
        {
          try
            {
                CustomerAddress cs = db.CustomerAddresses.FirstOrDefault(n => n.customerID == d.cusID && n.ID == d.id);
                cs.isActive = -1;
                db.CustomerAddresses.AddOrUpdate(cs);
                db.SaveChanges();
                return "Xóa địa chỉ thành công";
            }
            catch
            {
                return "Xóa địa chỉ thất bại";
            }
           
        }
        [HttpPut]
        public string update(address d)
        {
            try
            {
                CustomerAddress csmain = db.CustomerAddresses.FirstOrDefault(n => n.customerID == d.cusID&&n.isMain==1);
                csmain.isMain = 0;
                db.CustomerAddresses.AddOrUpdate(csmain);
                db.SaveChanges();
                CustomerAddress cs = db.CustomerAddresses.FirstOrDefault(n => n.customerID == d.cusID && n.ID == d.id);
                cs.isMain =1;
                db.CustomerAddresses.AddOrUpdate(cs);
                db.SaveChanges();
                return "Cập nhật địa chỉ thành công";
            }
            catch
            {
                return "Cập nhật thất bại";
            }

        }
    }
}
