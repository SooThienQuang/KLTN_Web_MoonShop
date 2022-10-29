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
        [ResponseType(typeof(List<Customer>))]
        public HttpResponseMessage GetUser(HttpRequestMessage request)
        {
          try
            {
                
                return request.CreateResponse(HttpStatusCode.OK, db.Customers.ToList());
            }
            catch
            {
                return request.CreateResponse(HttpStatusCode.NotFound,"Lỗi hệ thống");
            }
        }

        public HttpResponseMessage GetUser(HttpRequestMessage request, string username, string password)
        {
            Customer account = new Customer();
            account = db.Customers.FirstOrDefault(n => n.customerEmail.Equals(username) && n.customerPassword.Equals(password));
            if (account == null)
            {
                return request.CreateResponse(false);
            }
            else
            {
                return request.CreateResponse(account);
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
