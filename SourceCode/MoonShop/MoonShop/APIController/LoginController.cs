using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices.ComTypes;
using System.Web.Http;
using System.Web.Http.Description;
using MoonShop.Models;
namespace MoonShop.Controllers.APIController
{
    public class LoginController : ApiController
    {
        // GET: api/Login
        DBCosmeticEntities db = new DBCosmeticEntities();
        //public Account Get(string username,string password)
        //{
        //    Account account = new Account();
        //    account = db.Accounts.FirstOrDefault(n => n.username.Equals(username) && n.password.Equals(password));
        //    if(account == null)
        //    {
        //        Account a = new Account();
        //        a.username = username;
        //        a.password = password;
        //        db.Accounts.AddOrUpdate(a);
        //        db.SaveChanges();
        //    } 
        //    else
        //    {
        //        return account; 
        //    }    
        //    return account;
        //}
        public List<Account> Get()
        {

            return db.Accounts.ToList();            
        }

        [ResponseType(typeof(string))]
        public HttpResponseMessage GetUser(HttpRequestMessage request, string username, string password)
        {
            Account account = new Account();
            account = db.Accounts.FirstOrDefault(n => n.username.Equals(username) && n.password.Equals(password));
            if (account == null)
            {
                Account a = new Account();
                a.username = username;
                a.password = password;
                db.Accounts.AddOrUpdate(a);
                db.SaveChanges();
                return new HttpResponseMessage(HttpStatusCode.Created);
            }
            else
            {
                return request.CreateResponse(HttpStatusCode.OK,"Đăng nhập thành công");
            }
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
