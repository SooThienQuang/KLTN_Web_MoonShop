using KLTN_Web_MoonShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace KLTN_Web_MoonShop.Controllers.API
{
    public class employee
    {
        public long emID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string fullName { get; set; }
        public string sex { get; set; }
        public string mail { get; set; }
        public int posID { get; set; }
        public int roleID { get; set; }
        public long addressID { get; set; }
    }
    public class EmployeeController : ApiController
    {
        DBCosmeticEntities db = new DBCosmeticEntities();
        [HttpPost]
        public string add()
        {
            return "Success";
        }
    }
}
