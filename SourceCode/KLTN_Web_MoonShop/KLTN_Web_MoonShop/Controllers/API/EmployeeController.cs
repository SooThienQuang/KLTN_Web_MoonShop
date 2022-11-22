using KLTN_Web_MoonShop.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace KLTN_Web_MoonShop.Controllers.API
{
    public class employee
    {
        public long emID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string fullName { get; set; }
        public string lastName { get; set; }
        public DateTime birthday { get; set; }
        public string strDate { get; set; }
        public DateTime datecreate { get; set; }
        public string sex { get; set; }
        public string phone { get; set; }
        public string mail { get; set; }
        public int posID { get; set; }
        public int roleID { get; set; }
        public string address { get; set; }
        public string img { get; set; }
        public bool create { get; set; }
    }
    public class EmployeeController : ApiController
    {
        DBCosmeticEntities db = new DBCosmeticEntities();
        MD5 md5 = new MD5();
        [HttpPost]
        public string add(employee data)
        {
            //thêm mới nhân viên
            if(data.create==true)
            {
                string[] arrten = data.fullName.Split(' ');
                string nametam = arrten[arrten.Length - 1];
                for (int i = 0; i < arrten.Length - 1; i++)
                {
                    nametam = nametam + arrten[i].Substring(0, 1);
                }
                string username = "msc." + nametam.ToLower().Trim();
                string password = md5.CreateMD5("123");
                string idstr = DateTime.Now.ToString("yyyyMMddHHmmss");
                long id = long.Parse(idstr);
                Employee employee = new Employee();
                employee.emID = id;
                employee.UserName = username;
                employee.Password = password;
                employee.dateCreate = DateTime.Now;
                EmployeeDetail detail = new EmployeeDetail();
                detail.emID = id;
                detail.fullName = data.fullName;
                detail.birthday = data.birthday;
                detail.sex = data.sex;
                detail.mail = data.mail;
                detail.phone = data.phone;
                detail.posID = data.posID;
                detail.address = data.address;
                detail.photo = "Sample_User_Icon.png";
                detail.isActive = 1;
                db.Employees.AddOrUpdate(employee);
                db.EmployeeDetails.AddOrUpdate(detail);
                db.SaveChanges();

            }   
            else
            {
                EmployeeDetail detail = db.EmployeeDetails.FirstOrDefault(n => n.emID == data.emID);
                detail.fullName = data.fullName;
                detail.birthday = data.birthday;
                detail.sex = data.sex;
                detail.mail = data.mail;
                detail.phone = data.phone;
                detail.posID = data.posID;
                detail.address = data.address;
                detail.photo = "Sample_User_Icon.png";
                detail.isActive = 1;
                db.EmployeeDetails.AddOrUpdate(detail);
                db.SaveChanges();
            }    
           
            return "Success";
        }
        [Route("getemployee")]
        [HttpPost]
        public employee GetEmployee(employee id)
        {
            Employee employee = db.Employees.FirstOrDefault(n => n.emID == id.emID);
            EmployeeDetail ed=db.EmployeeDetails.FirstOrDefault(n=>n.emID == id.emID);
            employee e = new employee();
           try
            {
                e.emID = id.emID;
                e.phone = ed.phone;
                e.address = ed.address;
                string[] arrten = ed.fullName.Split(' ');
                int leng = ed.fullName.Length - (arrten[arrten.Length - 1]).Length;
                e.fullName = ed.fullName.Substring(0,leng-1);
                e.lastName = arrten[arrten.Length - 1];
                e.birthday = (DateTime)ed.birthday;
                e.strDate = e.birthday.ToString("yyyy-MM-dd");
                e.mail = ed.mail;
                e.UserName = employee.UserName;
                e.Password = employee.Password;
                e.sex = ed.sex;
                 e.posID = (int)ed.posID;
                e.datecreate = (DateTime)employee.dateCreate;
                e.img = ed.photo;
                return e;
            }
            catch
            {
                return e;
            }
          
        }
        
    }
}
