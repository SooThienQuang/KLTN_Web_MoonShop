using KLTN_Web_MoonShop.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Web.Http;
using System.Web.Http.Description;

namespace KLTN_Web_MoonShop.Controllers.API
{
    public class CustomerController : ApiController
    {
        // GET: api/Customer
        DBCosmeticEntities db = new DBCosmeticEntities();
        public string GetUser( string cusPhone, string cusPassword)
        {
            Customer account = new Customer();
            MD5 m=new MD5();
            string pass = m.CreateMD5(cusPassword);
            account=db.Customers.FirstOrDefault(n=>n.customerUserName.Equals(cusPhone) &&n.customerPassword.Equals(pass));
            if (account != null)
            {
                return "Đăng nhập thành công";
            }
            else
            {
                return "Username hoặc Password chưa chính xác ! Vui lòng thử lại";
            }
        }
        // GET: api/Customer/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Customer
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Customer/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Customer/5
        public void Delete(int id)
        {
        }
    }
}
