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
    public class data
    {
        public string id { get; set; }
    }
    public class imageController : ApiController
    {
        [HttpDelete]
        public string delete(data id)
        {
            return "thanh cong";
        }
    }
}
