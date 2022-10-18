using MoonShop.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MoonShop.Controllers
{
    public class ImageController : Controller
    {
        // GET: Image
        DBCosmeticEntities db = new DBCosmeticEntities();
        public ActionResult img(int id)
        {
            Account user = new Account();
            user= db.Accounts.FirstOrDefault(n => n.id == id);
            var dir = Server.MapPath("/Asset/img/");
            var path = Path.Combine(dir,user.img); //validate the path for security or use other means to generate the path.
            return base.File(path, "image/jpeg");
        }
    }
}