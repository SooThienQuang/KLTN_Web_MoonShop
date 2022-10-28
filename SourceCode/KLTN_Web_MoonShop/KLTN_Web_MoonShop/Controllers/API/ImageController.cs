using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KLTN_Web_MoonShop.Controllers.API
{
    public class ImageController : Controller
    {
        // GET: Image
        public ActionResult GetImage()
        {
            var dir = Server.MapPath("~/Asset/img/tu1.jpg");
            var path = Path.Combine(dir);
            return base.File(path, "image/jpeg");
        }
    }
}