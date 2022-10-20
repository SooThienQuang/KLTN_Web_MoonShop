using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MoonShop.Controllers
{
    public class PartialViewController : Controller
    {
        // GET: PartialView
        public ActionResult Navbar()
        {
            return PartialView();
        }
        public ActionResult NavbarTop()
        {
            return PartialView();
        }
    }
}