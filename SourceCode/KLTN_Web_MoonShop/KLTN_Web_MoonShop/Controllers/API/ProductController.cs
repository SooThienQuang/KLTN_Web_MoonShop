using KLTN_Web_MoonShop.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.ModelBinding;
namespace KLTN_Web_MoonShop.Controllers.API
{
    public class DataRequest
    {
        public long proID { get; set; }
        public long cusID { get; set; }
        public int quantity { get; set; }
    }
    public class ProductController : ApiController
    {
        DBCosmeticEntities db = new DBCosmeticEntities();
        // GET: api/Product
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Product/5
        public string Get(long id)
        {
            return "value";
        }

        // POST: api/Product
        [HttpPost]
        public void Post()
        {
        }

        // PUT: api/Product/5
        public void Put(int id, [FromBody]string value)
        {
        }
        [HttpDelete]
        public string Delete(DataRequest d)
        {
            try
            {
                Product pro = db.Products.FirstOrDefault(n => n.productID.Equals(d.proID));
                pro.isActive = 0;
                db.Products.AddOrUpdate(pro);
                db.SaveChanges();
                return "xóa thành công";
            }
            catch
            {
                return "Xóa thất bại";
            }
        }
    }
}
