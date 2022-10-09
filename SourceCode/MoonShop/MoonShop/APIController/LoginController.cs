using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MoonShop.Models;
namespace MoonShop.Controllers.APIController
{
    public class LoginController : ApiController
    {
        // GET: api/Login
        DBCosmeticEntities db = new DBCosmeticEntities();
        public IEnumerable<Tbl_Product> Get()
        {
            return db.Tbl_Product.ToList();
        }

        // GET: api/Login/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Login
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Login/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Login/5
        public void Delete(int id)
        {
        }
    }
}
