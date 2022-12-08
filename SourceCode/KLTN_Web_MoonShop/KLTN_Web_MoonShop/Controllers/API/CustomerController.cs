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
using System.Web.WebPages;

namespace KLTN_Web_MoonShop.Controllers.API
{
    public class requestCustomer
    {
        public long id { get; set; }
        public int sex { get; set; }
        public string fullname { get; set; }
        public string email { get; set; }
        public string address { get; set; }

    }
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
        [HttpPut]
        public string Put(requestCustomer data)
        {
            try
            {
                Customer customer = db.Customers.FirstOrDefault(n => n.customerID == data.id);
                if (customer != null)
                {
                    if (!data.fullname.IsEmpty())
                    {
                        customer.customerName = data.fullname;
                    }
                    if (!data.email.IsEmpty())
                    {
                        customer.customerMail = data.email;
                    }
                    customer.customerSex = data.sex;
                    customer.customerSex = data.sex;
                    db.Customers.AddOrUpdate(customer);
                    db.SaveChanges();
                }
                return "Cập nhật thông tin thành công";
            }
            catch
            {
                return "Cập nhật thông tin thất bại";
            }
            

        }

        // DELETE: api/Customer/5
        [HttpDelete]
        public string Delete(requestCustomer data)
        {
            Customer cus = db.Customers.FirstOrDefault(n =>n.customerID == data.id);
            if (cus!=null&&cus.isActive==1)
            {
                cus.isActive = -1;
                db.Customers.AddOrUpdate(cus);
                db.SaveChanges();
                return "Khóa thành công";
            }
            else
            {

                cus.isActive = 1;
                db.Customers.AddOrUpdate(cus);
                db.SaveChanges();
                return "Mở khóa thành công";
            }    
        }
    }
}
